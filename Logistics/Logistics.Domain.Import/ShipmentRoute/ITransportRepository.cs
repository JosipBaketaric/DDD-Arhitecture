namespace Logistics.Domain.Import.ShipmentRoute
{
    public interface ITransportRepository
    {
        Transport Get(int id);
        void Update(Transport transport);
    }
}
