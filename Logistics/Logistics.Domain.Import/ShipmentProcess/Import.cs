using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Import.ShipmentProcess
{
    public class Import
    {
        private Guid ShipmentProcessId;
        private ImportStatus StatusId = ImportStatus.Entry;
        private Location Origin;
        private Location Destination;
        
        public Import(Guid shipmentProcessId, ImportStatus statusId, Location origin, Location destination)
        {
            ShipmentProcessId = shipmentProcessId;
            StatusId = statusId;
            Origin = origin;
            Destination = destination;
        }
        
        internal void ShipmentArrivedOnTerminal(Location terminal)
        {
            if(Destination == terminal && StatusId == ImportStatus.Organized)
            {
                StatusId = ImportStatus.OnTerminal;
            }
        }
    }
}
