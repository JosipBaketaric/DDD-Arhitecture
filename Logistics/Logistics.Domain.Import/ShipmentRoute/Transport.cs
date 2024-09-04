using Logistics.Domain.Base;

namespace Logistics.Domain.Import.ShipmentRoute
{
    public class Transport : Aggregate
    {
        public Guid Id { get; private set; }
        internal int MaxLoadMass { get; private set; }
        internal int CurrentLoadMass { get; private set; }
        internal TransportStatus Status { get; private set; }

        internal Location DestinationTerminal { get; private set; }

        public Transport(Guid id, int maxLoadMass, int currentLoadMass, TransportStatus transportStatus, Location destinationTerminal)
        {
            Id = id;
            MaxLoadMass = maxLoadMass;
            CurrentLoadMass = currentLoadMass;            
            DestinationTerminal = destinationTerminal;
            Status = transportStatus;
        }

        public Transport(int maxLoadMass, Location destinationTerminal)
        {
            Id = Guid.NewGuid();
            MaxLoadMass = maxLoadMass;
            CurrentLoadMass = 0;
            DestinationTerminal = destinationTerminal;
            Status = TransportStatus.Entry;
        }
        
        private void ArriveOnTerminal(Location terminal) {
            if (terminal == DestinationTerminal)
            {
                Status = TransportStatus.Done;
            }
            else
            {
                Status = TransportStatus.OnTerminal;
            }            
        }

        public void ChangeStatus(TransportStatus status, Location terminal)
        {
            if(status == TransportStatus.OnTerminal)
            {
                if(terminal == null)
                {
                    throw new InvalidOperationException("Cannot change status to OnTerminal without terminal");
                }
                ArriveOnTerminal(terminal);
            } 
            else
            {
                Status = status;                
            }
            Console.WriteLine("Transport {0} status changed to {1}", Id, Status);
            this.RaiseDomainEvent(new TransportStatusChangedDomainEvent(Id, Status, terminal));
        }
    }
}
