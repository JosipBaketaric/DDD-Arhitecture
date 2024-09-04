using System;
using Logistics.Domain.Base;

namespace Logistics.Domain.Import.ShipmentRoute;

public class TransportStatusChangedDomainEventHandler: IDomainEventHandler<TransportStatusChangedDomainEvent>
{
    private readonly IShipmentRouteRepository _shipmentRouteRepository;
    public TransportStatusChangedDomainEventHandler(IShipmentRouteRepository shipmentRouteRepository)
    {
        _shipmentRouteRepository = shipmentRouteRepository;
    }
    public void Handle(TransportStatusChangedDomainEvent domainEvent) {
        Console.WriteLine("Transport status changed event handler called.");
        if (domainEvent.TransportStatus == TransportStatus.OnTerminal || domainEvent.TransportStatus == TransportStatus.Done)
        {
            var shipmentRoutes = _shipmentRouteRepository.GetShipmentRoutesForTransport(domainEvent.TransportId);
            foreach (var shipmentRoute in shipmentRoutes)
            {
                shipmentRoute.ArrivedOnLocation(domainEvent.Location);
                _shipmentRouteRepository.Update(shipmentRoute);
            }
        }
    }
}
