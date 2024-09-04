using Logistics.Domain.Base;
using Logistics.Domain.Import.ShipmentProcess;
using Logistics.Domain.Import.ShipmentRoute;

namespace Logistics.Domain.Import
{
    public class ShipmentCreatedDomainEventHandler: IDomainEventHandler<ShipmentCreatedDomainEvent>
    {
        private readonly IShipmentRouteRepository shipmentRouteRepository;

        public ShipmentCreatedDomainEventHandler(IShipmentRouteRepository shipmentRouteRepository)
        {
            this.shipmentRouteRepository = shipmentRouteRepository;
        }

        public void Handle(ShipmentCreatedDomainEvent domainEvent)
        {
            Console.WriteLine("Shipment created event handler called.");
            var routeSegmentIndex = 1;
            if(domainEvent.Shipment.Import != null)
            {
                var shipmentRoute = new ShipmentRoute.ShipmentRoute(
                    domainEvent.Shipment.ShipmentId,
                    domainEvent.Shipment.Mass,
                    domainEvent.Shipment.Import.Origin, 
                    domainEvent.Shipment.Import.Destination,
                    routeSegmentIndex);
                shipmentRouteRepository.Add(shipmentRoute);
                Console.WriteLine("Import route created");
                routeSegmentIndex++;

            }
            if(domainEvent.Shipment.Distribution != null)
            {
                var shipmentRoute = new ShipmentRoute.ShipmentRoute(
                    domainEvent.Shipment.ShipmentId,
                    domainEvent.Shipment.Mass,
                    domainEvent.Shipment.Distribution.Origin,
                    domainEvent.Shipment.Distribution.Destination,
                    routeSegmentIndex);
                shipmentRouteRepository.Add(shipmentRoute);
                Console.WriteLine("Distribution route created");
                routeSegmentIndex++;
            }

        }
    }
}
