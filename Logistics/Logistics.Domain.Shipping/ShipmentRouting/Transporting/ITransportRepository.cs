namespace Logistics.Domain.Shipping.ShipmentRouting.Transporting
{
    public interface ITransportRepository
    {
        Transport Get(Guid id);
        void Update(Transport transport);
        void Add(Transport transport);
    }
}
