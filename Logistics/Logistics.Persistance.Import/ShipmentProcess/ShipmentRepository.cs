using Logistics.Domain.Import.ShipmentProcess;
using Logistics.Domain.Import.ShipmentRoute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Persistance.Import.ShipmentProcess
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
