using Logistics.Domain.Shipping;
using Logistics.Domain.Shipping.ShipmentProcessing.ImportProcess;

namespace Logistics.Application.Command.Shipping.ShipmentProcess
{
    public class ChangeImportStatusCommand
    {
        public Guid ShipmentId { get; private set; }
        public ImportStatus Status { get; private set; }
        public Location Location { get; private set; }

        public ChangeImportStatusCommand(Guid shipmentId, ImportStatus status, Location location)
        {
            ShipmentId = shipmentId;
            Status = status;
            Location = location;
        }
    }
}
