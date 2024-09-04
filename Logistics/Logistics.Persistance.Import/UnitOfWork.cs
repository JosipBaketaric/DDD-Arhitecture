using Logistics.Application.Command.Import;
using Logistics.Domain.Base;

namespace Logistics.Persistance.Import;

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

        var domainEvents = InMemoryDbContext.Aggregates.SelectMany(x => x.GetDomainEvents()).ToList();
        foreach(var aggregate in InMemoryDbContext.Aggregates)
        {
            aggregate.ClearDomainEvents();
        }
        foreach (var domainEvent in domainEvents)
        {
            dispatcher.Dispatch(domainEvent);
        }
        // events can add more aggregates with their events
        if(InMemoryDbContext.Aggregates.Any(x => x.GetDomainEvents().Any()))
        {
            HandleEvents();
        }
    }
}
