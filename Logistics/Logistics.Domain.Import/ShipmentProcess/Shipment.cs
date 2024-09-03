using Logistics.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Import.ShipmentProcess
{
    public class Shipment: Aggregate
    {
        private int ShipmentId;
        private Import Import;
        private WarehouseReceiving WarehouseReceiving;
        private Distribution Distribution;

        public Shipment(int shipmentId, 
            int? importProcessId, 
            ImportStatus? importStatus, 
            int? warehouseReceivingProcessId, 
            WarehouseReceivingStatus? warehouseReceivingStatus,
            Location? warehouseTerminal,
            int? distributionProcessId,
            DistributionStatus? distributionStatus,
            Location? distributionTerminal)
        {
            ShipmentId = shipmentId;
            if(importProcessId.HasValue)
            {
                Import = new Import(importProcessId.Value, importStatus.Value);
            }
            if(warehouseReceivingProcessId.HasValue)
            {
                WarehouseReceiving = new WarehouseReceiving(warehouseReceivingProcessId.Value, warehouseReceivingStatus.Value, warehouseTerminal);
            }
            if(distributionProcessId.HasValue)
            {
                Distribution = new Distribution(distributionProcessId.Value, distributionStatus.Value, distributionTerminal);
            }            
        }        
        public void ImportStatusChange(ImportStatus importStatus, Location location)
        {
            if(Import == null)
            {
                throw new InvalidOperationException("Import process is not started");
            }
            Import.ShipmentArrivedOnTerminal(location);

            if(importStatus == ImportStatus.OnTerminal)
            {
                if(WarehouseReceiving != null)
                {
                    WarehouseReceiving.ShipmentArrivedOnTerminal(location);
                }
                if (Distribution != null)
                {
                    Distribution.ShipmentArrivedOnTerminal(location, WarehouseReceiving?.StatusId);
                }
            }
        }

    }
}
