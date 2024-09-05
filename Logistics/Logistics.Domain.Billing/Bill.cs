using Logistics.Domain.Base;

namespace Logistics.Domain.Billing
{
    public class Bill: Aggregate
    {
        public Guid ShipmentId { get; }
        public int Mass { get; }
        public string PostalCodeFrom { get; }
        public string PostalCodeTo { get; }

        public Bill(Guid shipmentId, int mass, string postalCodeFrom, string postalCodeTo)
        {
            ShipmentId = shipmentId;
            Mass = mass;
            PostalCodeFrom = postalCodeFrom;
            PostalCodeTo = postalCodeTo;
            Console.WriteLine("Bill created");
        }
    }
}
