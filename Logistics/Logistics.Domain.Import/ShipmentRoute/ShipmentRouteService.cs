namespace Logistics.Domain.Import.ShipmentRoute
{
    public class ShipmentRouteService    
    {
        private readonly IShipmentRouteRepository shipmentRouteRepository;
        public ShipmentRouteService(
            IShipmentRouteRepository shipmentRouteRepository
        ) 
        {
            this.shipmentRouteRepository = shipmentRouteRepository;
        }
        public void CoverByTransport(ShipmentRoute shipmentRoute, Transport transport)
        {
            if(shipmentRouteRepository.IsTransportOnShipmentRoute(shipmentRoute.ShipmentId, transport.Id))
            {
                throw new System.Exception("Transport already assigned to shipment shipment");
            }
            if(transport.CurrentLoadMass + shipmentRoute.ShipmentMass > transport.MaxLoadMass)
            {
                throw new System.Exception("Transport max mass exeeded");
            }
            shipmentRoute.CoverShipmentRouteByTransport(transport);
        }
    }
}