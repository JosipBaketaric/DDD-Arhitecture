using Logistics.Domain.Import;

namespace Logistics.Application.Command.Import
{
    public class ShipmentTransportService
    {
        private readonly ITransportRepository _transportRepository;
        private readonly IShipmentRepository _shipmentRepository;
        private readonly Domain.Import.ShipmentTransportService _shipmentTransportService;
        private readonly IUnitOfWork _unitOfWork;
        public ShipmentTransportService(
            ITransportRepository transportRepository, 
            IShipmentRepository shipmentRepository, 
            Domain.Import.ShipmentTransportService shipmentTransportService,
            IUnitOfWork unitOfWork)
        {
            _transportRepository = transportRepository;
            _shipmentRepository = shipmentRepository;
            _shipmentTransportService = shipmentTransportService;
            _unitOfWork = unitOfWork;
        }

        public void AddShipmentToTransport(AddShipmentToTransportCommand addShipmentToTransportCommand)
        {
            var transport = _transportRepository.Get(addShipmentToTransportCommand.TransportId);
            var shipment = _shipmentRepository.Get(addShipmentToTransportCommand.ShipmentId);

            _shipmentTransportService.AddShipmentToTransport(transport, shipment);

            _shipmentRepository.Update(shipment);
            _transportRepository.Update(transport);

            _unitOfWork.Commit();
        }
    }
}
