using System;
using Logistics.Domain.Import.ShipmentRoute;

namespace Logistics.Persistance.Import.ShipmentRoute;

public class TransportRepository : ITransportRepository
{
    public Transport Get(int id)
    {
        return InMemoryDbContext.Aggregates.OfType<Domain.Import.ShipmentRoute.Transport>().FirstOrDefault(x => x.Id == id);
    }

    public void Update(Transport transport)
    {
        
    }
}
