using Logistics.Domain.Shipping;
using Logistics.Domain.Shipping.ShipmentProcessing.DistributionProcess;

namespace Logistics.Application.Command.Shipping.ShipmentProcess
{
    public class ChangeDistributionStatusCommand
    {
        public Guid ShipmentId { get; private set; }
        public DistributionStatus Status { get; private set; }
        public Location Location { get; private set; }

        public ChangeDistributionStatusCommand(Guid shipmentId, DistributionStatus status, Location location)
        {
            ShipmentId = shipmentId;
            Status = status;
            Location = location;
        }
    }
}
