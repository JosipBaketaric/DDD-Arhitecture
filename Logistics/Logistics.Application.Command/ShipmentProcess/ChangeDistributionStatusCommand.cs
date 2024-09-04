using Logistics.Domain.Import;
using Logistics.Domain.Import.ShipmentProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.Command.Import.ShipmentProcess
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
