namespace Logistics.Domain.Import
{
    public class Transport
    {
        internal int Id { get; private set; }
        internal string VehicleRegistrationNumber { get; private set; }
        internal int MaxLoadMass { get; private set; }
        internal int CurrentLoadMass { get; private set; }

        private ICollection<Terminal> route;        
        public Transport(string vehicleRegistrationNumber, int maxLoadMass, int currentLoadMass, ICollection<Terminal> route)
        {
            VehicleRegistrationNumber = vehicleRegistrationNumber;
            MaxLoadMass = maxLoadMass;
            CurrentLoadMass = currentLoadMass;
            this.route = route;
        }
        internal void AddShipment(Shipment shipment)
        {
            if(!route.Contains(shipment.OriginTerminal))
            {
                throw new InvalidOperationException("The shipment cannot be added because the origin terminal is not on the route.");
            }
            if(!route.Contains(shipment.DestinationTerminal))
            {
                throw new InvalidOperationException("The shipment cannot be added because the destination terminal is not on the route.");
            }
            if (shipment.Weight + CurrentLoadMass > MaxLoadMass)
            {
                throw new InvalidOperationException("The shipment cannot be added because it exceeds the maximum load mass.");
            }
            CurrentLoadMass += shipment.Weight;            
        }
    }
}
