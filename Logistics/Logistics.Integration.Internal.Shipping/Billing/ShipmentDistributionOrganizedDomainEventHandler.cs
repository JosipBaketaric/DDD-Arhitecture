using Logistics.Domain.Base;
using Logistics.Domain.Shipping.ShipmentProcessing.DistributionProcess;
using Logistics.Domain.Shipping.ShipmentProcessing.Repositories;

namespace Logistics.Integration.Internal.Shipping.Billing
{
    public class ShipmentDistributionOrganizedDomainEventHandler : IDomainEventHandler<ShipmentDistributionOrganizedDomainEvent>
    {
        private readonly IShipmentRepository shipmentRepository;
        private readonly IBillingIntegrationService billingIntegrationService;

        public ShipmentDistributionOrganizedDomainEventHandler(IShipmentRepository shipmentRepository, IBillingIntegrationService billingIntegrationService)
        {
            this.shipmentRepository = shipmentRepository;
            this.billingIntegrationService = billingIntegrationService;
        }

        public void Handle(ShipmentDistributionOrganizedDomainEvent domainEvent)
        {
            Console.WriteLine("Shipment distribution organized in billing event handler.");
            var shipment = shipmentRepository.Get(domainEvent.ShipmentId);
            billingIntegrationService.CreateDistributionBillForShipment(domainEvent.ShipmentId, shipment.Mass, domainEvent.Origin.PostalCode, domainEvent.Destination.PostalCode);
        }
    }
}
