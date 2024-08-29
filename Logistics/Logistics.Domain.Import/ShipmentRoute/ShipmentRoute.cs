namespace Logistics.Domain.Import.ShipmentRoute
{
    public class ShipmentRoute
    {
        internal int ShipmentId {get; private set;}
        internal int ShipmentMass {get; private set;}
        private int transportId;
        private Location from;
        private Location to;
        private int routeSegmentIndex;
        private ShipmentRouteStatus status = ShipmentRouteStatus.Unassigned;        

        internal void CoverShipmentRouteByTransport(Transport transport)
        {
            transportId = transport.Id;
            status = ShipmentRouteStatus.Assigned;
        }
    }

    internal enum ShipmentRouteStatus
    {
        Unassigned,
        Assigned,
        InProgress,
        Done
    }
}