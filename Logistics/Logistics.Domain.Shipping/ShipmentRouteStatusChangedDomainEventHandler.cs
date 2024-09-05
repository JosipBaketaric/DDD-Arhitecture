using Logistics.Domain.Base;
using Logistics.Domain.Shipping.ShipmentProcessing.ImportProcess;
using Logistics.Domain.Shipping.ShipmentProcessing.Repositories;
using Logistics.Domain.Shipping.ShipmentRouting;

namespace Logistics.Domain.Shipping
{
    public class ShipmentRouteStatusChangedDomainEventHandler: IDomainEventHandler<ShipmentRouteStatusChangeDomainEvent>
    {
        private readonly IShipmentRepository shipmentRepository;

        public ShipmentRouteStatusChangedDomainEventHandler(IShipmentRepository shipmentRepository)
        {
            this.shipmentRepository = shipmentRepository;
        }

        public void Handle(ShipmentRouteStatusChangeDomainEvent domainEvent)
        {
            Console.WriteLine("Shipment route done event handler called.");
            if(domainEvent.ShipmentProcessType == ShipmentProcessType.Import)
            {
                var shipment = shipmentRepository.Get(domainEvent.ShipmentId);
                if (domainEvent.Status == ShipmentRouteStatus.Done)
                {
                    shipment.ImportStatusChange(ImportStatus.OnTerminal, domainEvent.Terminal);
                }
                else if (domainEvent.Status == ShipmentRouteStatus.InProgress)
                {
                    shipment.ImportStatusChange(ImportStatus.OnTransport, domainEvent.Terminal);
                }                    
            }            
        }
    }
}
