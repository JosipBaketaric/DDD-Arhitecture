using System;

namespace Logistics.Domain.Base;

public interface IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
    void Handle(TDomainEvent domainEvent);
}