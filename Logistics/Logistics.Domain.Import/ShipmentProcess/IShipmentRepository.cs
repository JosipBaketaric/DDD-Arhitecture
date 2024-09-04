using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Import.ShipmentProcess
{
    public interface IShipmentRepository
    {
        Shipment Get(Guid shipmentId);
        void Add(Shipment shipment);
        void Update(Shipment shipment);
    }
}
