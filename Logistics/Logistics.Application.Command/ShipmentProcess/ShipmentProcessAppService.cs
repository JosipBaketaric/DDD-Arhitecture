using Logistics.Domain.Import;
using Logistics.Domain.Import.ShipmentProcess;
using Logistics.Domain.Import.ShipmentRoute;

namespace Logistics.Application.Command.Import.ShipmentProcess
{
    public class ShipmentProcessAppService
    {
        private readonly IShipmentRepository shipmentRepository;
        private readonly IShipmentRouteRepository shipmentRouteRepository;
        private readonly CreateShipmentDomainService createShipmentDomainService;
        private readonly IUnitOfWork unitOfWork;
        public ShipmentProcessAppService(IShipmentRepository shipmentRepository, IUnitOfWork unitOfWork, CreateShipmentDomainService createShipmentDomainService, IShipmentRouteRepository shipmentRouteRepository)
        {
            this.shipmentRepository = shipmentRepository;
            this.unitOfWork = unitOfWork;
            this.createShipmentDomainService = createShipmentDomainService;
            this.shipmentRouteRepository = shipmentRouteRepository;
        }

        public (Guid, List<Guid>) CreateShipmentFromImport(CreateShipmentFromImportCommand command)
        {
            var shipmentAndShipmentRoutes = createShipmentDomainService.CreateShipmentInImport(
                command.Mass, 
                command.ShipmentOrigin, 
                command.ShipmentDestination, 
                command.ImportDestination, 
                command.UseWarehouse);

            shipmentRepository.Add(shipmentAndShipmentRoutes.Item1);
            foreach(var shipmentRoute in shipmentAndShipmentRoutes.Item2)
            {
                shipmentRouteRepository.Add(shipmentRoute);
            }   

            unitOfWork.Commit();

            return (shipmentAndShipmentRoutes.Item1.ShipmentId, shipmentAndShipmentRoutes.Item2.Select(x => x.ShipmentRouteId).ToList());
        }     
        
        public void ChangeImportStatus(ChangeImportStatusCommand command)
        {
            var shipment = shipmentRepository.Get(command.ShipmentId);

            shipment.ImportStatusChange(command.Status, command.Location);

            shipmentRepository.Update(shipment);

            unitOfWork.Commit();
        }

        public void ChangeWarehouseReceivingStatus(ChangeWarehouseReceivingCommand command)
        {
            var shipment = shipmentRepository.Get(command.ShipmentId);

            shipment.WarehouseReceivingStatusChange(command.Status, command.Location);

            shipmentRepository.Update(shipment);

            unitOfWork.Commit();
        }

        public void ChangeDistributionStatus(ChangeDistributionStatusCommand command)
        {
            var shipment = shipmentRepository.Get(command.ShipmentId);

            shipment.DistributionStatusChange(command.Status, command.Location);

            shipmentRepository.Update(shipment);

            unitOfWork.Commit();
        }
    }
}
