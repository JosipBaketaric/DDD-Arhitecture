using Logistics.Domain.Base;

namespace Logistics.Domain.Import.ShipmentRoute
{
    public class ShipmentRoute: Aggregate
    {
        public Guid ShipmentRouteId { get; private set; }
        public Guid ShipmentId {get; private set;}
        internal int ShipmentMass {get; private set;}
        public Guid TransportId { get; private set; }
        private Location from;
        private Location to;
        private int routeSegmentIndex;
        private ShipmentRouteStatus status = ShipmentRouteStatus.Unassigned;
        private ShipmentProcessType ShipmentProcessType;

        public ShipmentRoute(Guid shipmentRouteId, Guid shipmentId, int shipmentMass, Location from, Location to, int routeSegmentIndex, ShipmentProcessType shipmentProcessType)
        {
            ShipmentRouteId = shipmentRouteId;
            ShipmentId = shipmentId;
            ShipmentMass = shipmentMass;
            this.from = from;
            this.to = to;
            this.routeSegmentIndex = routeSegmentIndex;
            ShipmentProcessType = shipmentProcessType;
        }

        public ShipmentRoute(Guid shipmentId, int shipmentMass, Location from, Location to, int routeSegmentIndex, ShipmentProcessType shipmentProcessType)
        {
            ShipmentRouteId = Guid.NewGuid();
            ShipmentId = shipmentId;
            ShipmentMass = shipmentMass;
            this.from = from;
            this.to = to;
            this.routeSegmentIndex = routeSegmentIndex;
            ShipmentProcessType = shipmentProcessType;
        }

        internal void CoverShipmentRouteByTransport(Transport transport)
        {
            TransportId = transport.Id;
            status = ShipmentRouteStatus.Assigned;
            Console.WriteLine("Shipment {0} added to transport {1}", ShipmentId, TransportId);
        }

        internal void ArrivedOnLocation(Location location) {
            if(to == location) {
                status = ShipmentRouteStatus.Done;
                Console.WriteLine("Shipment {0} route done", ShipmentId);
                this.RaiseDomainEvent(new ShipmentRouteDoneDomainEvent(
                    ShipmentId,
                    ShipmentRouteId,
                    location,
                    ShipmentProcessType
                    ));
            }
        }
    }

    
}