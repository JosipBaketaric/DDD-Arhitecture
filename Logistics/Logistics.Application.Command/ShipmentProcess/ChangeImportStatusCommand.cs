using Logistics.Domain.Import;
using Logistics.Domain.Import.ShipmentProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.Command.Import.ShipmentProcess
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
