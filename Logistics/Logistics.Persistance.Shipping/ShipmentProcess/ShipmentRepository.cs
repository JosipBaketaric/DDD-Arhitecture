using Logistics.Domain.Shipping.ShipmentProcessing;
using Logistics.Domain.Shipping.ShipmentProcessing.Repositories;

namespace Logistics.Persistance.Shipping.ShipmentProcess
{
    public class ShipmentRepository : IShipmentRepository
    {
        public void Add(Shipment shipment)
        {
            InMemoryDbContext.Aggregates.Add(shipment);
        }

        public Shipment Get(Guid shipmentId)
        {
            return InMemoryDbContext.Aggregates.OfType<Shipment>().FirstOrDefault(x => x.ShipmentId == shipmentId);
        }

        public void Update(Shipment shipment)
        {
            
        }
    }
}
