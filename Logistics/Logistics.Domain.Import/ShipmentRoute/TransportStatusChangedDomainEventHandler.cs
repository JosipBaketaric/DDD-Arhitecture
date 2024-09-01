using System;
using Logistics.Domain.Base;

namespace Logistics.Domain.Import.ShipmentRoute;

public class TransportStatusChangedDomainEventHandler: IDomainEventHandler<TransportStatusChangedDomainEvent>
{
    public void Handle(TransportStatusChangedDomainEvent domainEvent) {
        Console.WriteLine("Event handled, status:" + domainEvent.TransportStatusId);
    }
}
