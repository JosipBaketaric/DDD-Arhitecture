using Logistics.Domain.Shipping;

namespace Logistics.Application.Command.Shipping.ShipmentProcess
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
