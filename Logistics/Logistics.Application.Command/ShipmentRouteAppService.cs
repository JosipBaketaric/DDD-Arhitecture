using Logistics.Domain.Import.ShipmentRoute;

namespace Logistics.Application.Command.Import
{
    public class ShipmentRouteAppService
    {
        private readonly ITransportRepository transportRepository;
        private readonly IShipmentRouteRepository shipmentRouteRepository;
        private readonly ShipmentRouteService shipmentRouteService;
        private readonly TransportStatusService transportStatusService;
        private readonly IUnitOfWork unitOfWork;
        public ShipmentRouteAppService(
            ITransportRepository transportRepository, 
            IShipmentRouteRepository shipmentRouteRepository, 
            ShipmentRouteService shipmentRouteService,
            TransportStatusService transportStatusService,
            IUnitOfWork unitOfWork)
        {
            this.transportRepository = transportRepository;
            this.shipmentRouteRepository = shipmentRouteRepository;
            this.shipmentRouteService = shipmentRouteService;
            this.unitOfWork = unitOfWork;
            this.transportStatusService = transportStatusService;
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
            var transport = transportRepository.Get(changeTransportStatusCommand.TransportId);
            var transportShipmentRoutes = shipmentRouteRepository.GetShipmentRoutesForTransport(changeTransportStatusCommand.TransportId);

            transportStatusService.ChangeTransportStatus(transport, changeTransportStatusCommand.TransportStatus);

            transportRepository.Update(transport);

        }
    }
}
