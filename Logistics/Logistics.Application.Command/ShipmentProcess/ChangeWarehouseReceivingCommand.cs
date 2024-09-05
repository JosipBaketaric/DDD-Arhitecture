using Logistics.Domain.Shipping;
using Logistics.Domain.Shipping.ShipmentProcessing.WarehouseReceivingProcess;

namespace Logistics.Application.Command.Shipping.ShipmentProcess
{
    public class ChangeWarehouseReceivingCommand
    {
        public Guid ShipmentId { get; private set; }
        public WarehouseReceivingStatus Status { get; private set; }
        public Location Location { get; private set; }

        public ChangeWarehouseReceivingCommand(Guid shipmentId, WarehouseReceivingStatus status, Location location)
        {
            ShipmentId = shipmentId;
            Status = status;
            Location = location;
        }
    }
}
