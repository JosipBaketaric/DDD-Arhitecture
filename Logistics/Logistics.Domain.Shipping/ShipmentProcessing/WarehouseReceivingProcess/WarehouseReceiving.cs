using Logistics.Domain.Base;

namespace Logistics.Domain.Shipping.ShipmentProcessing.WarehouseReceivingProcess
{
    public class WarehouseReceiving: Entity
    {
        private Guid ShipmentProcessId;
        public Guid ShipmentId { get; private set; }
        internal WarehouseReceivingStatus StatusId { get; private set; }
        private Location WarehouseTerminal;

        public WarehouseReceiving(Guid shipmentProcessId, WarehouseReceivingStatus statusId, Location warehouseTerminal, Guid shipmentId)
        {
            ShipmentProcessId = shipmentProcessId;
            StatusId = statusId;
            WarehouseTerminal = warehouseTerminal;
            ShipmentId = shipmentId;
        }

        internal void ShipmentArrivedOnTerminal(Location terminal)
        {
            if (WarehouseTerminal == terminal && StatusId == WarehouseReceivingStatus.Organized)
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
            if(status == WarehouseReceivingStatus.Organized)
            {
                RaiseDomainEvent(new ShipmentWarehouseReceivingOrganizedDomainEvent(ShipmentProcessId, WarehouseTerminal, ShipmentId));
            }
        }
    }
}
