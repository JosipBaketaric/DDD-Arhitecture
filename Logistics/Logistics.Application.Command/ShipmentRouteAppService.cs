using Logistics.Domain.Import.ShipmentRoute;

namespace Logistics.Application.Command.Import
{
    public class ShipmentRouteAppService
    {
        private readonly ITransportRepository transportRepository;
        private readonly IShipmentRouteRepository shipmentRouteRepository;
        private readonly ShipmentRouteService shipmentRouteService;
        private readonly IUnitOfWork unitOfWork;
        public ShipmentRouteAppService(
            ITransportRepository transportRepository, 
            IShipmentRouteRepository shipmentRouteRepository, 
            ShipmentRouteService shipmentRouteService,
            IUnitOfWork unitOfWork)
        {
            this.transportRepository = transportRepository;
            this.shipmentRouteRepository = shipmentRouteRepository;
            this.shipmentRouteService = shipmentRouteService;
            this.unitOfWork = unitOfWork;        
        }

        public void AddShipmentToTransport(CoverShipmentRouteByTransportCommand coverShipmentRouteByTransportCommand)
        {
            var transport = transportRepository.Get(coverShipmentRouteByTransportCommand.TransportId);
            var shipmentRoute = shipmentRouteRepository.Get(coverShipmentRouteByTransportCommand.ShipmentRouteId);

            shipmentRouteService.CoverByTransport(shipmentRoute, transport);

            shipmentRouteRepository.Update(shipmentRoute);            

            unitOfWork.Commit();
        }

        public void ChangeTransportStatus(ChangeTransportStatusCommand changeTransportStatusCommand)
        {
            Console.WriteLine("App service started");

            var transport = transportRepository.Get(changeTransportStatusCommand.TransportId);

            Console.WriteLine("Call status change");
            
            transport.ArriveOnTerminal(changeTransportStatusCommand.Location);

            Console.WriteLine("Status changed app service");
            
            transportRepository.Update(transport);

            unitOfWork.Commit();
        }
    }
}
