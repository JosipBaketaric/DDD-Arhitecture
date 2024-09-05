using Logistics.Domain.Shipping.ShipmentRouting.Transporting;

namespace Logistics.Persistance.Shipping.ShipmentRouting;

public class TransportRepository : ITransportRepository
{
    public Transport Get(Guid id)
    {
        return InMemoryDbContext.Aggregates.OfType<Transport>().FirstOrDefault(x => x.Id == id);
    }

    public void Update(Transport transport)
    {
        
    }

    public void Add(Transport transport)
    {
        InMemoryDbContext.Aggregates.Add(transport);
    }
}
