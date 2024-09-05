using Logistics.Domain.Base;

namespace Logistics.Domain.Shipping.ShipmentProcessing.ImportProcess
{
    public class Import: Entity
    {
        private Guid ShipmentProcessId;
        private Guid ShipmentId;
        private ImportStatus StatusId = ImportStatus.Entry;
        internal Location Origin { get; }
        internal Location Destination { get; }

        public Import(Guid shipmentProcessId, Guid shipmentId, ImportStatus statusId, Location origin, Location destination)
        {
            ShipmentProcessId = shipmentProcessId;
            ShipmentId = shipmentId;
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
            if(status == ImportStatus.Organized)
            {
                RaiseDomainEvent(new ShipmentImportOrganizedDomainEvent(ShipmentProcessId, Origin, Destination, ShipmentId));
            }            
        }
    }
}
