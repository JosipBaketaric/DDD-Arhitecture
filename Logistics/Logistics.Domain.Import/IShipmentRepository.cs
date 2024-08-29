namespace Logistics.Domain.Import
{
    public interface IShipmentRepository
    {
        void Add(Shipment shipment);
        void Update(Shipment shipment);
        Shipment Get(int id);
    }
}
