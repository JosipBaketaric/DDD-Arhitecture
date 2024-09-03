using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Import.ShipmentProcess
{
    public class Import
    {
        private int ShipmentProcessId;
        private ImportStatus StatusId;
        private Location Destination;
        private Location ShipmentDestination;

        public Import(int shipmentProcessId, ImportStatus statusId, Location destination)
        {
            ShipmentProcessId = shipmentProcessId;
            StatusId = statusId;
            Destination = destination;
        }

        internal void ShipmentArrivedOnTerminal(Location terminal)
        {
            if(Destination == terminal && StatusId == ImportStatus.Organized)
            {
                if(terminal == ShipmentDestination)
                {
                    StatusId = ImportStatus.Delivered;
                }
                StatusId = ImportStatus.OnTerminal;
            }
        }
    }
}
