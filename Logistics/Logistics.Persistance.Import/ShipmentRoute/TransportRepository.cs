using System;
using Logistics.Domain.Import.ShipmentRoute;

namespace Logistics.Persistance.Import.ShipmentRoute;

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
