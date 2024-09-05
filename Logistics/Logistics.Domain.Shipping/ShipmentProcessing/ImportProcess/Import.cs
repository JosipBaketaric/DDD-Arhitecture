using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Shipping.ShipmentProcessing.ImportProcess
{
    public class Import
    {
        private Guid ShipmentProcessId;
        private ImportStatus StatusId = ImportStatus.Entry;
        internal Location Origin { get; }
        internal Location Destination { get; }

        public Import(Guid shipmentProcessId, ImportStatus statusId, Location origin, Location destination)
        {
            ShipmentProcessId = shipmentProcessId;
            StatusId = statusId;
            Origin = origin;
            Destination = destination;
        }

        internal void ShipmentArrivedOnTerminal(Location terminal)
        {
            if (Destination == terminal && StatusId == ImportStatus.OnTransport)
            {
                StatusId = ImportStatus.OnTerminal;
                Console.WriteLine("Import Status changed to " + StatusId);
            }
        }

        internal void StatusChanged(ImportStatus status)
        {
            if (status == ImportStatus.OnTerminal)
            {
                throw new InvalidOperationException("Cannot change status to OnTerminal directly");
            }
            StatusId = status;
            Console.WriteLine("Import Status changed to " + status);
        }
    }
}
