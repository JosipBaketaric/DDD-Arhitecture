namespace Logistics.Application.Command.Import
{
    public class CoverShipmentRouteByTransportCommand
    {
        public int TransportId { get; private set; }
        public Guid ShipmentRouteId { get; private set; }

        public CoverShipmentRouteByTransportCommand(int transportId, Guid shipmentRouteId)
        {
            TransportId = transportId;
            ShipmentRouteId = shipmentRouteId;
        }
    }
}