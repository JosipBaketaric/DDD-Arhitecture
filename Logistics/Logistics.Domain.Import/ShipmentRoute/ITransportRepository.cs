namespace Logistics.Domain.Import.ShipmentRoute
{
    public interface ITransportRepository
    {
        Transport Get(Guid id);
        void Update(Transport transport);
        void Add(Transport transport);
    }
}
