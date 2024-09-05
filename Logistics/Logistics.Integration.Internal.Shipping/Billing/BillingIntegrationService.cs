using Logistics.Domain.Billing;

namespace Logistics.Integration.Internal.Shipping.Billing
{
    public class BillingIntegrationService: IBillingIntegrationService
    {
        public void CreateImportBillForShipment(Guid shipmentId, int mass, string postalCodeFrom, string postalCodeTo)
        {
            var bill = new Bill(shipmentId, mass, postalCodeFrom, postalCodeTo);
        }

        public void CreateDistributionBillForShipment(Guid shipmentId, int mass, string postalCodeFrom, string postalCodeTo)
        {
            var bill = new Bill(shipmentId, mass, postalCodeFrom, postalCodeTo);
        }
    }
}
