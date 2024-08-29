namespace Logistics.Domain.Import
{
    public class Transport
    {
        internal int Id { get; private set; }
        internal int MaxLoadMass { get; private set; }
        internal int CurrentLoadMass { get; private set; }

        public Transport(int id, int maxLoadMass, int currentLoadMass, ICollection<Terminal> route)
        {
            Id = id;
            MaxLoadMass = maxLoadMass;
            CurrentLoadMass = currentLoadMass;            
        }       
    }
}
