namespace Logistics.Domain.Import
{
    public interface ITransportRepository
    {
        void Add(Transport shipment);
        void Update(Transport shipment);
        Transport Get(int id);
    }
}
