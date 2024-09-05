using Logistics.Domain.Base;

namespace Logistics.Domain.Shipping.ShipmentProcessing.ImportProcess
{
    public class ShipmentImportOrganizedDomainEvent: IDomainEvent
    {
        public Guid ShipmentProcessId { get; }
        public Guid ShipmentId { get; }
        public Location Origin { get; }
        public Location Destination { get; }

        public ShipmentImportOrganizedDomainEvent(Guid shipmentProcessId, Location origin, Location destination, Guid shipmentId)
        {
            ShipmentProcessId = shipmentProcessId;
            Origin = origin;
            Destination = destination;            
            ShipmentId = shipmentId;
        }
    }
}
