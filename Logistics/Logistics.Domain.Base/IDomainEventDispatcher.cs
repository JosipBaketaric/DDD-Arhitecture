using System;

namespace Logistics.Domain.Base;

public interface IDomainEventDispatcher
{
    void Dispatch(IDomainEvent domainEvent);
}
