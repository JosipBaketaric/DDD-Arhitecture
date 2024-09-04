namespace Logistics.Application.Command.Import
{
    public class CoverShipmentRouteByTransportCommand
    {
        public Guid TransportId { get; private set; }
        public Guid ShipmentRouteId { get; private set; }

        public CoverShipmentRouteByTransportCommand(Guid transportId, Guid shipmentRouteId)
        {
            TransportId = transportId;
            ShipmentRouteId = shipmentRouteId;
        }
    }
}