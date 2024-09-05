using Logistics.Domain.Shipping.ShipmentRouting;

namespace Logistics.Persistance.Shipping.ShipmentRouting;

public class ShipmentRouteRepository : IShipmentRouteRepository
{
    public ShipmentRoute Get(Guid shipmentRouteId)
    {
        return InMemoryDbContext.Aggregates.OfType<ShipmentRoute>().FirstOrDefault(x => x.ShipmentRouteId == shipmentRouteId);
    }

    public IEnumerable<ShipmentRoute> GetShipmentRoutesForTransport(Guid transportId)
    {
        return InMemoryDbContext.Aggregates.OfType<ShipmentRoute>().Where(x => x.TransportId == transportId);
    }

    public bool IsTransportOnShipmentRoute(Guid shipmentId, Guid transportId)
    {
        return false;
    }

    public void Update(ShipmentRoute shipmentRoute)
    {
        
    }

    public void Add(ShipmentRoute shipmentRoute)
    {
        InMemoryDbContext.Aggregates.Add(shipmentRoute);
    }
}
