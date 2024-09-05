using Logistics.Domain.Base;

namespace Logistics.Domain.Shipping.ShipmentProcessing.WarehouseReceivingProcess
{
    public class ShipmentWarehouseReceivingOrganizedDomainEvent : IDomainEvent
    {
        public Guid ShipmentProcessId { get; }
        public Guid ShipmentId { get; }
        public Location Warehouse { get; }

        public ShipmentWarehouseReceivingOrganizedDomainEvent(Guid shipmentProcessId, Location warehouse, Guid shipmentId)
        {
            ShipmentProcessId = shipmentProcessId;            
            ShipmentId = shipmentId;
            Warehouse = warehouse;
        }
    }
}
