using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Shipping.ShipmentProcessing.WarehouseReceivingProcess
{
    public class WarehouseReceiving
    {
        private Guid ShipmentProcessId;
        internal WarehouseReceivingStatus StatusId { get; private set; }
        private Location WarehouseTerminal;

        public WarehouseReceiving(Guid shipmentProcessId, WarehouseReceivingStatus statusId, Location warehouseTerminal)
        {
            ShipmentProcessId = shipmentProcessId;
            StatusId = statusId;
            WarehouseTerminal = warehouseTerminal;
        }

        internal void ShipmentArrivedOnTerminal(Location terminal)
        {
            if (WarehouseTerminal == terminal && StatusId == WarehouseReceivingStatus.ReadyForLoading)
            {
                StatusId = WarehouseReceivingStatus.OnTerminal;
                Console.WriteLine("WarehouseReceiving Status changed to " + StatusId);
            }
        }

        internal void StatusChanged(WarehouseReceivingStatus status)
        {
            if (status == WarehouseReceivingStatus.OnTerminal)
            {
                throw new InvalidOperationException("Cannot change status to OnTerminal directly");
            }
            StatusId = status;
            Console.WriteLine("WarehouseReceiving Status changed to " + status);
        }
    }
}
