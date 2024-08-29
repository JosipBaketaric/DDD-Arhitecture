namespace Logistics.Application.Command.Import
{
    public class AddShipmentToTransportCommand
    {
        public int TransportId { get; set; }
        public int ShipmentId { get; set; }
    }
}