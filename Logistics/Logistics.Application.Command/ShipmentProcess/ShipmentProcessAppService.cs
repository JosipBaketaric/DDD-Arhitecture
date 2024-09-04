using Logistics.Domain.Import;
using Logistics.Domain.Import.ShipmentProcess;
using Logistics.Domain.Import.ShipmentRoute;

namespace Logistics.Application.Command.Import.ShipmentProcess
{
    public class ShipmentProcessAppService
    {
        private readonly IShipmentRepository shipmentRepository;
        private readonly IUnitOfWork unitOfWork;
        public ShipmentProcessAppService(IShipmentRepository shipmentRepository, IUnitOfWork unitOfWork)
        {
            this.shipmentRepository = shipmentRepository;
            this.unitOfWork = unitOfWork;
        }

        public Guid CreateShipmentFromImport(CreateShipmentFromImportCommand command)
        {
            var shipment = new Shipment(command.Mass, command.ShipmentOrigin, command.ShipmentDestination, command.ImportDestination, command.UseWarehouse);

            shipmentRepository.Add(shipment);

            unitOfWork.Commit();

            return shipment.ShipmentId;
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
