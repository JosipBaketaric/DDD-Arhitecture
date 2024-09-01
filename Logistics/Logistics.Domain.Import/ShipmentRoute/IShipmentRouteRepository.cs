namespace Logistics.Domain.Import.ShipmentRoute
{
    public interface IShipmentRouteRepository {
        bool IsTransportOnShipmentRoute(int shipmentId, int transportId);
        ShipmentRoute Get(int shipmentRouteId);
        void Update(ShipmentRoute shipmentRoute);
        IEnumerable<ShipmentRoute> GetShipmentRoutesForTransport(int transportId);
    }
}