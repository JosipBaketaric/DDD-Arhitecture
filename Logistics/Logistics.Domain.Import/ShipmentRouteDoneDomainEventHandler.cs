using Logistics.Domain.Base;
using Logistics.Domain.Import.ShipmentProcess;
using Logistics.Domain.Import.ShipmentRoute;

namespace Logistics.Domain.Import
{
    public class ShipmentRouteDoneDomainEventHandler: IDomainEventHandler<ShipmentRouteDoneDomainEvent>
    {
        private readonly IShipmentRepository shipmentRepository;

        public ShipmentRouteDoneDomainEventHandler(IShipmentRepository shipmentRepository)
        {
            this.shipmentRepository = shipmentRepository;
        }

        public void Handle(ShipmentRouteDoneDomainEvent domainEvent)
        {
            Console.WriteLine("Shipment route done event handler called.");
            if(domainEvent.ShipmentProcessType == ShipmentProcessType.Import)
            {
                var shipment = shipmentRepository.Get(domainEvent.ShipmentId);
                shipment.ImportStatusChange(ImportStatus.OnTerminal, domainEvent.Terminal);
            }            
        }
    }
}
