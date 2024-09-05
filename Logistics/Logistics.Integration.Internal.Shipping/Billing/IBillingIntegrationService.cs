namespace Logistics.Integration.Internal.Shipping.Billing
{
    public interface IBillingIntegrationService
    {
        void CreateImportBillForShipment(Guid ShipmentId, int Mass, string postalCodeFrom, string postalCodeTo);

        void CreateDistributionBillForShipment(Guid ShipmentId, int Mass, string postalCodeFrom, string postalCodeTo);
    }
}
