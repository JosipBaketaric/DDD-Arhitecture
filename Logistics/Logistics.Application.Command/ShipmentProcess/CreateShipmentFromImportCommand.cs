using Logistics.Domain.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.Command.Import.ShipmentProcess
{
    public class CreateShipmentFromImportCommand
    {
        public Location ShipmentOrigin { get; private set; }
        public Location ShipmentDestination { get; private set; }
        public Location ImportDestination { get; private set; }
        public bool UseWarehouse { get; private set; }
        public int Mass { get; private set; }

        public CreateShipmentFromImportCommand(int mass, Location shipmentOrigin, Location shipmentDestination, Location importDestination, bool useWarehouse)
        {
            ShipmentOrigin = shipmentOrigin;
            ShipmentDestination = shipmentDestination;
            ImportDestination = importDestination;
            UseWarehouse = useWarehouse;
            Mass = mass;
        }
    }
}
