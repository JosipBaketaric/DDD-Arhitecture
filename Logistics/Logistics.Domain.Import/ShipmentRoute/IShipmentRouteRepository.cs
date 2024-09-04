namespace Logistics.Domain.Import.ShipmentRoute
{
    public interface IShipmentRouteRepository {
        bool IsTransportOnShipmentRoute(Guid shipmentId, int transportId);
        ShipmentRoute Get(Guid shipmentRouteId);
        void Update(ShipmentRoute shipmentRoute);
        IEnumerable<ShipmentRoute> GetShipmentRoutesForTransport(int transportId);
        void Add(ShipmentRoute shipmentRoute);
    }
}