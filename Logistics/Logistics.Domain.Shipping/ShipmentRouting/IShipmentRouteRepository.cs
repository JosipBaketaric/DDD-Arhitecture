namespace Logistics.Domain.Shipping.ShipmentRouting
{
    public interface IShipmentRouteRepository
    {
        bool IsTransportOnShipmentRoute(Guid shipmentId, Guid transportId);
        ShipmentRoute Get(Guid shipmentRouteId);
        void Update(ShipmentRoute shipmentRoute);
        IEnumerable<ShipmentRoute> GetShipmentRoutesForTransport(Guid transportId);
        void Add(ShipmentRoute shipmentRoute);
    }
}