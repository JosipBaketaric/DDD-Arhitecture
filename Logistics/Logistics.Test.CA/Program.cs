using Logistics.Domain.Base;
using Logistics.Domain.Import.ShipmentRoute;
using Microsoft.Extensions.DependencyInjection;
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Main started");
        // Set up the DI container
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        // Build the service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var transport = new Transport();
        transport.StatusChange();

        var dispatcher = new DomainEventDispatcher(serviceProvider);

        foreach(var domainEvent in transport.GetDomainEvents()) {
            dispatcher.Dispatch(domainEvent);
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {        
        services.AddTransient<IDomainEventHandler<TransportStatusChangedDomainEvent>, TransportStatusChangedDomainEventHandler>();
        
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
    }
}