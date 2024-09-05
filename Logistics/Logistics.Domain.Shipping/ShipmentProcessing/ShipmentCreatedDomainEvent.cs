using Logistics.Domain.Base;

namespace Logistics.Domain.Shipping.ShipmentProcessing
{
    public class ShipmentCreatedDomainEvent : IDomainEvent
    {
        public ShipmentCreatedDomainEvent(Shipment shipment)
        {
            Shipment = shipment;
        }

        public Shipment Shipment { get; }
    }
}
