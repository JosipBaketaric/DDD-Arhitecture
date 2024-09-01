using System;
using Logistics.Domain.Import.ShipmentRoute;

namespace Logistics.Persistance.Import.ShipmentRoute;

public class TransportRepository : ITransportRepository
{
    public Transport Get(int id)
    {
        return new Transport();
    }

    public void Update(Transport transport)
    {
        InMemoryDbContext.AddData(transport);
    }
}
