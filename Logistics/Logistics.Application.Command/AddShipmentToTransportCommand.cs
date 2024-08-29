namespace Logistics.Application.Command.Import
{
    public class CoverShipmentRouteByTransportCommand
    {
        public int TransportId { get; set; }
        public int ShipmentRouteId { get; set; }
    }
}