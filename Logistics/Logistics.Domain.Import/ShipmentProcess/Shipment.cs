using Logistics.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Import.ShipmentProcess
{
    public class Shipment: Aggregate
    {
        private Guid ShipmentId;
        private Import Import;
        private WarehouseReceiving WarehouseReceiving;
        private Distribution Distribution;

        public Shipment(Guid shipmentId, 
            Location ShipmentOrigin,
            Location shipmentDestination,
            Guid? importProcessId, 
            ImportStatus? importStatus, 
            Location? importDestination,
            Guid? warehouseReceivingProcessId, 
            WarehouseReceivingStatus? warehouseReceivingStatus,
            Location? warehouseTerminal,
            Guid? distributionProcessId,
            DistributionStatus? distributionStatus,
            Location? distributionOriginTerminal,
            Location? distributionDestinationTerminal)
        {
            ShipmentId = shipmentId;
            if(importProcessId.HasValue)
            {
                Import = new Import(importProcessId.Value, importStatus.Value, ShipmentOrigin, importDestination);
            }
            if(warehouseReceivingProcessId.HasValue)
            {
                WarehouseReceiving = new WarehouseReceiving(warehouseReceivingProcessId.Value, warehouseReceivingStatus.Value, warehouseTerminal);
            }
            if(distributionProcessId.HasValue)
            {
                Distribution = new Distribution(distributionProcessId.Value, distributionStatus.Value, distributionOriginTerminal, distributionDestinationTerminal);
            }            
        }

        public static Shipment CreateShipmentFromImport(
            Location shipmentOrigin,
            Location shipmentDestination,
            Location? importDestination,
            bool usesWarehouse
        ) {
            bool hasDistribution = false;
            if(shipmentDestination.Country == "HR") {
                hasDistribution = true;
            }

            return new Shipment(
                Guid.NewGuid(),
                shipmentOrigin,
                shipmentDestination,
                Guid.NewGuid(),
                ImportStatus.Entry,
                importDestination,
                usesWarehouse ? Guid.NewGuid() : null,
                usesWarehouse ? WarehouseReceivingStatus._201 : null,
                usesWarehouse ? importDestination : null,
                hasDistribution ? Guid.NewGuid() : null,
                hasDistribution ? DistributionStatus.EntryInProgress : null,
                hasDistribution ? importDestination : null,
                hasDistribution ? shipmentDestination : null);
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
