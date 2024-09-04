using Logistics.Application.Command.Import;
using Logistics.Domain.Base;
using Logistics.Domain.Import.ShipmentRoute;
using Logistics.Domain.Import.ShipmentProcess;
using Logistics.Domain.Import;
using Logistics.Persistance.Import;
using Logistics.Persistance.Import.ShipmentRoute;
using Microsoft.Extensions.DependencyInjection;
using Logistics.Persistance.Import.ShipmentProcess;
using Logistics.Application.Command.Import.ShipmentProcess;
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
        
        //AddShipmentToTransport(serviceProvider);
        CreateShipment(serviceProvider);

        Console.ReadLine();
    }

    private static void AddShipmentToTransport(ServiceProvider serviceProvider)
    {
        InMemoryDbContext.SetupData();

        var shipmentRouteDomainService = new ShipmentRouteService(serviceProvider.GetService<IShipmentRouteRepository>());

        var appService = new ShipmentRouteAppService(
            serviceProvider.GetService<ITransportRepository>(),
            serviceProvider.GetService<IShipmentRouteRepository>(),
            shipmentRouteDomainService,
            serviceProvider.GetService<IUnitOfWork>()
        );

        appService.AddShipmentToTransport(new CoverShipmentRouteByTransportCommand(1, Guid.NewGuid()));

        appService.ChangeTransportStatus(new ChangeTransportStatusCommand(1, 2, new Logistics.Domain.Import.Location("HR", "Zagreb", "10000", "Terminal 2")));
    }

    private static void CreateShipment(ServiceProvider serviceProvider)
    {
        var shipmentProcessAppService = new ShipmentProcessAppService(
            serviceProvider.GetService<IShipmentRepository>(),
            serviceProvider.GetService<IUnitOfWork>());

        var warehouseTerminal = new Logistics.Domain.Import.Location("HR", "Split", "21000", "Terminal 2");

        var shipmentId = shipmentProcessAppService.CreateShipmentFromImport(new CreateShipmentFromImportCommand(
            100,
            new Logistics.Domain.Import.Location("HR", "Zagreb", "10000", "Terminal 1"),
            warehouseTerminal,
            new Logistics.Domain.Import.Location("HR", "Split", "21000", "Terminal 3"),
            true));

        shipmentProcessAppService.ChangeImportStatus(new ChangeImportStatusCommand(shipmentId, ImportStatus.Organized, null));

        shipmentProcessAppService.ChangeWarehouseReceivingStatus(new ChangeWarehouseReceivingCommand(shipmentId, WarehouseReceivingStatus.ReadyForLoading, null));

        shipmentProcessAppService.ChangeDistributionStatus(new ChangeDistributionStatusCommand(shipmentId, DistributionStatus.Organized, null));

        shipmentProcessAppService.ChangeImportStatus(new ChangeImportStatusCommand(shipmentId, ImportStatus.OnTransport, null));

        shipmentProcessAppService.ChangeImportStatus(new ChangeImportStatusCommand(shipmentId, ImportStatus.OnTerminal, warehouseTerminal));

        foreach (var aggregate in InMemoryDbContext.Aggregates)
        {
            Console.WriteLine("Aggregate: {0}", aggregate.GetType().Name);
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {        
        services.AddTransient<IShipmentRepository, ShipmentRepository>();
        services.AddTransient<IShipmentRouteRepository, ShipmentRouteRepository>();
        services.AddTransient<ITransportRepository, TransportRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        
        services.AddTransient<IDomainEventHandler<TransportStatusChangedDomainEvent>, TransportStatusChangedDomainEventHandler>();
        services.AddTransient<IDomainEventHandler<ShipmentCreatedDomainEvent>, ShipmentCreatedDomainEventHandler>();

        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
    }
}