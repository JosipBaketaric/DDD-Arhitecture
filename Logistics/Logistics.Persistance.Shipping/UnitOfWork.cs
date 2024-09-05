using Logistics.Application.Command.Shipping;
using Logistics.Domain.Base;

namespace Logistics.Persistance.Shipping;

public class UnitOfWork : IUnitOfWork
{
    private readonly IServiceProvider _serviceProvider;

    // Constructor injection of IServiceProvider
    public UnitOfWork(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Commit()
    {
        HandleEvents();       

        Console.WriteLine("Commit");
    }

    private void HandleEvents()
    {
        var dispatcher = new DomainEventDispatcher(_serviceProvider);

        var domainEvents = InMemoryDbContext.Entities.SelectMany(x => x.GetDomainEvents()).ToList();
        foreach(var entity in InMemoryDbContext.Entities)
        {
            entity.ClearDomainEvents();
        }
        foreach (var domainEvent in domainEvents)
        {
            dispatcher.Dispatch(domainEvent);
        }
        // events can add more aggregates with their events
        if(InMemoryDbContext.Entities.Any(x => x.GetDomainEvents().Any()))
        {
            HandleEvents();
        }
    }
}
