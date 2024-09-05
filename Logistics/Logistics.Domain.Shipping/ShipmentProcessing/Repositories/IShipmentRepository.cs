using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistics.Domain.Shipping.ShipmentProcessing;

namespace Logistics.Domain.Shipping.ShipmentProcessing.Repositories
{
    public interface IShipmentRepository
    {
        Shipment Get(Guid shipmentId);
        void Add(Shipment shipment);
        void Update(Shipment shipment);
    }
}
