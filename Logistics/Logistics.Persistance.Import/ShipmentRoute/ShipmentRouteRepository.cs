

using Logistics.Domain.Import.ShipmentRoute;

namespace Logistics.Persistance.Import.ShipmentRoute;

public class ShipmentRouteRepository : IShipmentRouteRepository
{
    public Domain.Import.ShipmentRoute.ShipmentRoute Get(int shipmentRouteId)
    {
        return InMemoryDbContext.Aggregates.OfType<Domain.Import.ShipmentRoute.ShipmentRoute>().FirstOrDefault(x => x.ShipmentId == shipmentRouteId);
    }

    public IEnumerable<Domain.Import.ShipmentRoute.ShipmentRoute> GetShipmentRoutesForTransport(int transportId)
    {
        return InMemoryDbContext.Aggregates.OfType<Domain.Import.ShipmentRoute.ShipmentRoute>().Where(x => x.TransportId == transportId);
    }

    public bool IsTransportOnShipmentRoute(int shipmentId, int transportId)
    {
        return false;
    }

    public void Update(Domain.Import.ShipmentRoute.ShipmentRoute shipmentRoute)
    {
        
    }
}
