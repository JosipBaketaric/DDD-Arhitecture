using Logistics.Domain.Shipping.ShipmentProcessing;
using Logistics.Domain.Shipping.ShipmentProcessing.Repositories;

namespace Logistics.Persistance.Shipping.ShipmentProcess
{
    public class ShipmentRepository : IShipmentRepository
    {
        public void Add(Shipment shipment)
        {
            InMemoryDbContext.Entities.Add(shipment);
            InMemoryDbContext.Entities.Add(shipment.Import);
            InMemoryDbContext.Entities.Add(shipment.Distribution);
        }

        public Shipment Get(Guid shipmentId)
        {
            return InMemoryDbContext.Entities.OfType<Shipment>().FirstOrDefault(x => x.ShipmentId == shipmentId);
        }

        public void Update(Shipment shipment)
        {
            
        }
    }
}
