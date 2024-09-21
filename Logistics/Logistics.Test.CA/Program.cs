using Logistics.Application.Command.Shipping;
using Logistics.Domain.Base;
using Logistics.Domain.Shipping;
using Microsoft.Extensions.DependencyInjection;
using Logistics.Application.Command.Shipping.ShipmentProcess;
using Logistics.Domain.Shipping.ShipmentRouting;
using Logistics.Domain.Shipping.ShipmentRouting.Transporting;
using Logistics.Domain.Shipping.ShipmentProcessing.ImportProcess;
using Logistics.Domain.Shipping.ShipmentProcessing.WarehouseReceivingProcess;
using Logistics.Domain.Shipping.ShipmentProcessing.Repositories;
using Logistics.Domain.Shipping.ShipmentProcessing;
using Logistics.Application.Command.Shipping.ShipmentRoute;
using Logistics.Domain.Shipping.ShipmentProcessing.DistributionProcess;
using Logistics.Persistance.Shipping.ShipmentProcess;
using Logistics.Persistance.Shipping.ShipmentRouting;
using Logistics.Persistance.Shipping;
using Logistics.Integration.Internal.Shipping.Billing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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

        shipmentProcessAppService.ChangeWarehouseReceivingStatus(new ChangeWarehouseReceivingCommand(shipmentId, WarehouseReceivingStatus.Organized, null));

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
        services.AddTransient<IBillingIntegrationService, BillingIntegrationService>();

        services.AddTransient<IDomainEventHandler<TransportStatusChangedDomainEvent>, TransportStatusChangedDomainEventHandler>();
        services.AddTransient<IDomainEventHandler<ShipmentRouteStatusChangeDomainEvent>, ShipmentRouteStatusChangedDomainEventHandler>();
        services.AddTransient<IDomainEventHandler<ShipmentImportOrganizedDomainEvent>, ShipmentImportOrganizedDomainEventHandler>();
        services.AddTransient<IDomainEventHandler<ShipmentDistributionOrganizedDomainEvent>, ShipmentDistributionOrganizedDomainEventHandler>();

        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();

        services.AddDbContext<ShippingDbContext>(options =>
        {
            options.UseSqlServer("Server=localhost; Database=Logistics; Integrated Security=True; trustServerCertificate=true");
        });
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                ConfigureServices(services);
            });
}