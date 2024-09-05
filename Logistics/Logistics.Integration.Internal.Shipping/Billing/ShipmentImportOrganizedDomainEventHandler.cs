using Logistics.Domain.Base;
using Logistics.Domain.Shipping.ShipmentProcessing.ImportProcess;
using Logistics.Domain.Shipping.ShipmentProcessing.Repositories;

namespace Logistics.Integration.Internal.Shipping.Billing
{
    public class ShipmentImportOrganizedDomainEventHandler : IDomainEventHandler<ShipmentImportOrganizedDomainEvent>
    {
        private readonly IShipmentRepository shipmentRepository;
        private readonly IBillingIntegrationService billingIntegrationService;

        public ShipmentImportOrganizedDomainEventHandler(IShipmentRepository shipmentRepository, IBillingIntegrationService billingIntegrationService)
        {
            this.shipmentRepository = shipmentRepository;
            this.billingIntegrationService = billingIntegrationService;
        }

        public void Handle(ShipmentImportOrganizedDomainEvent domainEvent)
        {
            Console.WriteLine("Shipment import organized in billing event handler.");
            var shipment = shipmentRepository.Get(domainEvent.ShipmentId);
            billingIntegrationService.CreateImportBillForShipment(domainEvent.ShipmentId, shipment.Mass, domainEvent.Origin.PostalCode, domainEvent.Destination.PostalCode);
        }
    }
}
