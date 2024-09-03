using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Import.ShipmentProcess
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
            if(WarehouseTerminal == terminal && StatusId == WarehouseReceivingStatus._202)
            {
                StatusId = WarehouseReceivingStatus._203;
            }
        }
    }
}
