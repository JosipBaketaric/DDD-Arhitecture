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
        Scenario1(serviceProvider);

        Console.ReadLine();
    }   

    private static void Scenario1(ServiceProvider serviceProvider)
    {
        // service initialization
        var createShipmentDomainService = new CreateShipmentDomainService();
        var shipmentProcessAppService = new ShipmentProcessAppService(
            serviceProvider.GetService<IShipmentRepository>(),
            serviceProvider.GetService<IUnitOfWork>(),
            createShipmentDomainService,
            serviceProvider.GetService<IShipmentRouteRepository>()
            );

        var shipmentRouteDomainService = new ShipmentRouteService(serviceProvider.GetService<IShipmentRouteRepository>());

        var shipmentRouteAppService = new ShipmentRouteAppService(
            serviceProvider.GetService<ITransportRepository>(),
            serviceProvider.GetService<IShipmentRouteRepository>(),
            shipmentRouteDomainService,
            serviceProvider.GetService<IUnitOfWork>()
        );

        // scenario start
        var warehouseTerminal = new Location("HR", "Split", "21000", "Terminal 2");

        var shipmentAndShipmentRouteIds = shipmentProcessAppService.CreateShipmentFromImport(new CreateShipmentFromImportCommand(
            100,
            new Location("HR", "Zagreb", "10000", "Terminal 1"),
            new Location("HR", "Split", "21000", "Terminal 3"),
            warehouseTerminal,
            true));

        var transportId = shipmentRouteAppService.CreateTransport(new CreateTransportCommand(100, warehouseTerminal));

        var shipmentId = shipmentAndShipmentRouteIds.Item1;
        var importShipmentRouteId = shipmentAndShipmentRouteIds.Item2[0];

        shipmentProcessAppService.ChangeImportStatus(new ChangeImportStatusCommand(shipmentId, ImportStatus.Organized, null));

        shipmentRouteAppService.ChangeTransportStatus(new ChangeTransportStatusCommand(transportId, TransportStatus.Organized, null));

        shipmentRouteAppService.AddShipmentToTransport(new CoverShipmentRouteByTransportCommand(transportId, importShipmentRouteId));        

        shipmentProcessAppService.ChangeWarehouseReceivingStatus(new ChangeWarehouseReceivingCommand(shipmentId, WarehouseReceivingStatus.ReadyForLoading, null));

        shipmentProcessAppService.ChangeDistributionStatus(new ChangeDistributionStatusCommand(shipmentId, DistributionStatus.Organized, null));

        shipmentRouteAppService.ChangeTransportStatus(new ChangeTransportStatusCommand(transportId, TransportStatus.Driving, null));

        shipmentRouteAppService.ChangeTransportStatus(new ChangeTransportStatusCommand(transportId, TransportStatus.OnTerminal, warehouseTerminal));
    }

    private static void ConfigureServices(IServiceCollection services)
    {        
        services.AddTransient<IShipmentRepository, ShipmentRepository>();
        services.AddTransient<IShipmentRouteRepository, ShipmentRouteRepository>();
        services.AddTransient<ITransportRepository, TransportRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        
        services.AddTransient<IDomainEventHandler<TransportStatusChangedDomainEvent>, TransportStatusChangedDomainEventHandler>();
        services.AddTransient<IDomainEventHandler<ShipmentRouteStatusChangeDomainEvent>, ShipmentRouteStatusChangedDomainEventHandler>();

        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
    }
}