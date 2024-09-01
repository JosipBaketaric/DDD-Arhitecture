using Logistics.Domain.Base;

namespace Logistics.Domain.Import.ShipmentRoute
{
    public class Transport : Aggregate
    {
        internal int Id { get; private set; }
        internal int MaxLoadMass { get; private set; }
        internal int CurrentLoadMass { get; private set; }        
        
        public Transport() {

        }

        public void StatusChange() {
            this.RaiseDomainEvent(new TransportStatusChangedDomainEvent(1,2));
        }
    }
}
