using Logistics.Domain.Base;

namespace Logistics.Domain.Import.ShipmentRoute
{
    public class ShipmentRoute: Aggregate
    {
        public int ShipmentId {get; private set;}
        internal int ShipmentMass {get; private set;}
        public int TransportId { get; private set; }
        private Location from;
        private Location to;
        private int routeSegmentIndex;
        private ShipmentRouteStatus status = ShipmentRouteStatus.Unassigned;        

        public ShipmentRoute(int shipmentId, int shipmentMass, Location from, Location to, int routeSegmentIndex)
        {
            ShipmentId = shipmentId;
            ShipmentMass = shipmentMass;
            this.from = from;
            this.to = to;
            this.routeSegmentIndex = routeSegmentIndex;
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
            }
        }
    }

    
}