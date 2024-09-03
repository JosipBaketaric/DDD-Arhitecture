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
        var dispatcher = new DomainEventDispatcher(_serviceProvider);


        foreach(var domainEvent in InMemoryDbContext.Aggregates.SelectMany(x => x.GetDomainEvents())) {
            dispatcher.Dispatch(domainEvent);
        }

        Console.WriteLine("Commit");
    }
}
