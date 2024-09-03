using Logistics.Domain.Base;

namespace Logistics.Domain.Import.ShipmentRoute
{
    public class Transport : Aggregate
    {
        public int Id { get; private set; }
        internal int MaxLoadMass { get; private set; }
        internal int CurrentLoadMass { get; private set; }

        internal int TransportStatus { get; private set; } = 1;

        public Transport(int id, int maxLoadMass, int currentLoadMass)
        {
            Id = id;
            MaxLoadMass = maxLoadMass;
            CurrentLoadMass = currentLoadMass;            
        }
        
        public void ArriveOnTerminal(Location terminal) {
            TransportStatus = 2;
            Console.WriteLine("Transport {0} status changed to {1}", Id, TransportStatus);
            this.RaiseDomainEvent(new TransportStatusChangedDomainEvent(Id, TransportStatus, terminal));
        }
    }
}
