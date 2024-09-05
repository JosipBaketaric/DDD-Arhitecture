using Logistics.Domain.Base;
using Logistics.Domain.Shipping.ShipmentRouting.Transporting;

namespace Logistics.Domain.Shipping.ShipmentRouting
{
    public class ShipmentRoute : Aggregate
    {
        public Guid ShipmentRouteId { get; private set; }
        public Guid ShipmentId { get; private set; }
        internal int ShipmentMass { get; private set; }
        public Guid TransportId { get; private set; }
        private Location From;
        private Location To;
        private int RouteSegmentIndex;
        private ShipmentRouteStatus Status = ShipmentRouteStatus.Unassigned;
        private ShipmentProcessType ShipmentProcessType;

        public ShipmentRoute(Guid shipmentRouteId, Guid shipmentId, int shipmentMass, Location from, Location to, int routeSegmentIndex, ShipmentProcessType shipmentProcessType)
        {
            ShipmentRouteId = shipmentRouteId;
            ShipmentId = shipmentId;
            ShipmentMass = shipmentMass;
            From = from;
            To = to;
            RouteSegmentIndex = routeSegmentIndex;
            ShipmentProcessType = shipmentProcessType;
        }

        public ShipmentRoute(Guid shipmentId, int shipmentMass, Location from, Location to, int routeSegmentIndex, ShipmentProcessType shipmentProcessType)
        {
            ShipmentRouteId = Guid.NewGuid();
            ShipmentId = shipmentId;
            ShipmentMass = shipmentMass;
            From = from;
            To = to;
            RouteSegmentIndex = routeSegmentIndex;
            ShipmentProcessType = shipmentProcessType;
        }

        internal void CoverShipmentRouteByTransport(Transport transport)
        {
            TransportId = transport.Id;
            Status = ShipmentRouteStatus.Assigned;
            Console.WriteLine("Shipment {0} added to transport {1}", ShipmentId, TransportId);
        }

        internal void InProgress()
        {
            Status = ShipmentRouteStatus.InProgress;
            Console.WriteLine("Shipment {0} route in progress", ShipmentId);
            RaiseDomainEvent(new ShipmentRouteStatusChangeDomainEvent(
                    ShipmentId,
                    ShipmentRouteId,
                    null,
                    ShipmentProcessType,
                    Status
                    ));
        }

        internal void ArrivedOnLocation(Location location)
        {
            if (To == location)
            {
                Status = ShipmentRouteStatus.Done;
                Console.WriteLine("Shipment {0} route done", ShipmentId);
                RaiseDomainEvent(new ShipmentRouteStatusChangeDomainEvent(
                    ShipmentId,
                    ShipmentRouteId,
                    location,
                    ShipmentProcessType,
                    Status
                    ));
            }
        }
    }


}