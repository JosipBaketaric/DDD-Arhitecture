namespace Logistics.Domain.Import.ShipmentRoute
{
    public class Transport
    {
        internal int Id { get; private set; }
        internal int MaxLoadMass { get; private set; }
        internal int CurrentLoadMass { get; private set; }        
    }
}
