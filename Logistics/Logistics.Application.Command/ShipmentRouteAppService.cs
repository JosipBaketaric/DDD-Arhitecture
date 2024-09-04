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

        public Guid CreateTransport(CreateTransportCommand command)
        {
            var transport = new Transport(command.MaxMass, command.Destination);
            transportRepository.Add(transport);
            unitOfWork.Commit();
            return transport.Id;
        }

        public void AddShipmentToTransport(CoverShipmentRouteByTransportCommand coverShipmentRouteByTransportCommand)
        {
            var transport = transportRepository.Get(coverShipmentRouteByTransportCommand.TransportId);
            var shipmentRoute = shipmentRouteRepository.Get(coverShipmentRouteByTransportCommand.ShipmentRouteId);

            shipmentRouteService.CoverByTransport(shipmentRoute, transport);

            shipmentRouteRepository.Update(shipmentRoute);            

            unitOfWork.Commit();
        }

        public void ChangeTransportStatus(ChangeTransportStatusCommand command)
        {
            var transport = transportRepository.Get(command.TransportId);
                        
            transport.ChangeStatus(command.TransportStatus, command.StatusLocation);
                        
            transportRepository.Update(transport);

            unitOfWork.Commit();
        }
    }
}
