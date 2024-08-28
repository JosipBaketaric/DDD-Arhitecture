namespace Logistics.Domain.Import
{
    public class Shipment
    {
        private int id;
        private int transportId;

        internal int Weight { get; private set; }

        internal Terminal OriginTerminal { get; private set; }

        internal Terminal DestinationTerminal { get; private set; }
        
        public Shipment(int weight, Terminal originTerminal, Terminal destinationTerminal)
        {
            Weight = weight;
            OriginTerminal = originTerminal;
            DestinationTerminal = destinationTerminal;
        }

        internal void SetTransport(int transportId)
        {
            this.transportId = transportId;
        }
    }
}
