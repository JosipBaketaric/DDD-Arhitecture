using System;
using Logistics.Domain.Base;
using Logistics.Domain.Shipping.ShipmentRouting;

namespace Logistics.Domain.Shipping.ShipmentRouting.Transporting;

public class TransportStatusChangedDomainEventHandler : IDomainEventHandler<TransportStatusChangedDomainEvent>
{
    private readonly IShipmentRouteRepository shipmentRouteRepository;
    public TransportStatusChangedDomainEventHandler(IShipmentRouteRepository shipmentRouteRepository)
    {
        this.shipmentRouteRepository = shipmentRouteRepository;
    }
    public void Handle(TransportStatusChangedDomainEvent domainEvent)
    {
        Console.WriteLine("Transport status changed event handler called.");
        if (domainEvent.TransportStatus == TransportStatus.Driving)
        {
            var shipmentRoutes = shipmentRouteRepository.GetShipmentRoutesForTransport(domainEvent.TransportId);
            foreach (var shipmentRoute in shipmentRoutes)
            {
                shipmentRoute.InProgress();
                shipmentRouteRepository.Update(shipmentRoute);
            }
        }
        if (domainEvent.TransportStatus == TransportStatus.OnTerminal || domainEvent.TransportStatus == TransportStatus.Done)
        {
            var shipmentRoutes = shipmentRouteRepository.GetShipmentRoutesForTransport(domainEvent.TransportId);
            foreach (var shipmentRoute in shipmentRoutes)
            {
                shipmentRoute.ArrivedOnLocation(domainEvent.Location);
                shipmentRouteRepository.Update(shipmentRoute);
            }
        }
    }
}
