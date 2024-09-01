using Logistics.Application.Command.Import;
using Logistics.Domain.Base;
using Logistics.Domain.Import.ShipmentRoute;
using Logistics.Persistance.Import;
using Logistics.Persistance.Import.ShipmentRoute;
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

        var shipmentRouteDomainService = new ShipmentRouteService(serviceProvider.GetService<IShipmentRouteRepository>());        

        var appService = new ShipmentRouteAppService(
            serviceProvider.GetService<ITransportRepository>(),
            serviceProvider.GetService<IShipmentRouteRepository>(),
            shipmentRouteDomainService,
            serviceProvider.GetService<IUnitOfWork>()
        );

        appService.ChangeTransportStatus(new ChangeTransportStatusCommand());

        

        
    }

    private static void ConfigureServices(IServiceCollection services)
    {        
        services.AddTransient<IShipmentRouteRepository, ShipmentRouteRepository>();
        services.AddTransient<ITransportRepository, TransportRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        
        services.AddTransient<IDomainEventHandler<TransportStatusChangedDomainEvent>, TransportStatusChangedDomainEventHandler>();
        
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
    }
}