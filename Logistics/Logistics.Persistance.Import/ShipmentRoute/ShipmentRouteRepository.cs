

using Logistics.Domain.Import.ShipmentRoute;

namespace Logistics.Persistance.Import.ShipmentRoute;

public class ShipmentRouteRepository : IShipmentRouteRepository
{
    public Domain.Import.ShipmentRoute.ShipmentRoute Get(int shipmentRouteId)
    {
        return new Domain.Import.ShipmentRoute.ShipmentRoute();
    }

    public IEnumerable<Domain.Import.ShipmentRoute.ShipmentRoute> GetShipmentRoutesForTransport(int transportId)
    {
        var list = new List<Domain.Import.ShipmentRoute.ShipmentRoute>();
        list.Add(new Domain.Import.ShipmentRoute.ShipmentRoute());
        return list;
    }

    public bool IsTransportOnShipmentRoute(int shipmentId, int transportId)
    {
        return false;
    }

    public void Update(Domain.Import.ShipmentRoute.ShipmentRoute shipmentRoute)
    {
        InMemoryDbContext.AddData(shipmentRoute);
    }
}
