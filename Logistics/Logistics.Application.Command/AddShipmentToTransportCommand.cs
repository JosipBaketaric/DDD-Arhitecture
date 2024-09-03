namespace Logistics.Application.Command.Import
{
    public class CoverShipmentRouteByTransportCommand
    {
        public int TransportId { get; private set; }
        public int ShipmentRouteId { get; private set; }

        public CoverShipmentRouteByTransportCommand(int transportId, int shipmentRouteId)
        {
            TransportId = transportId;
            ShipmentRouteId = shipmentRouteId;
        }
    }
}