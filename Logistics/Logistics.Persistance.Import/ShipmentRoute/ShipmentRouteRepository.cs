

using Logistics.Domain.Import.ShipmentRoute;

namespace Logistics.Persistance.Import.ShipmentRoute;

public class ShipmentRouteRepository : IShipmentRouteRepository
{
    public Domain.Import.ShipmentRoute.ShipmentRoute Get(Guid shipmentRouteId)
    {
        return InMemoryDbContext.Aggregates.OfType<Domain.Import.ShipmentRoute.ShipmentRoute>().FirstOrDefault(x => x.ShipmentRouteId == shipmentRouteId);
    }

    public IEnumerable<Domain.Import.ShipmentRoute.ShipmentRoute> GetShipmentRoutesForTransport(Guid transportId)
    {
        return InMemoryDbContext.Aggregates.OfType<Domain.Import.ShipmentRoute.ShipmentRoute>().Where(x => x.TransportId == transportId);
    }

    public bool IsTransportOnShipmentRoute(Guid shipmentId, Guid transportId)
    {
        return false;
    }

    public void Update(Domain.Import.ShipmentRoute.ShipmentRoute shipmentRoute)
    {
        
    }

    public void Add(Domain.Import.ShipmentRoute.ShipmentRoute shipmentRoute)
    {
        InMemoryDbContext.Aggregates.Add(shipmentRoute);
    }
}
