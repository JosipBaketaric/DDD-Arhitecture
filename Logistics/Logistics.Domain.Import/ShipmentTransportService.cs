namespace Logistics.Domain.Import
{
    public class ShipmentTransportService
    {
        public void AddShipmentToTransport(Transport transport, Shipment shipment)
        {
            transport.AddShipment(shipment);
            shipment.SetTransport(transport.Id);
        }
    }
}
