using System;

namespace Logistics.Domain.Import.ShipmentRoute;

public class TransportState
{
    internal TransportStatus TransportStatus {get; private set;}

    
    internal void SetTransportStatus(TransportStatus transportStatus) {
        TransportStatus = transportStatus;

        // foreach(var shipmentRoute in transportShipmentRoutes) {
        //     shipmentRoute.ArrivedOnLocation(transportStatus.Location);
        // }
    }
    
    public void ChangeTransportStatus(IEnumerable<ShipmentRoute> transportShipmentRoutes, Transport transport, TransportStatus transportStatus) {
        // transport.SetTransportStatus(transportStatus);
        // var transportShipmentRoutes = shipmentRouteRepository.GetShipmentRoutesForTransport(transport.Id);
        
    }
}
