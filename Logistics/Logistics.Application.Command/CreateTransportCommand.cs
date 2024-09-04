using Logistics.Domain.Import;

namespace Logistics.Application.Command.Import
{
    public class CreateTransportCommand
    {
        public int MaxMass { get; private set; }
        public Location Destination { get; private set; }

        public CreateTransportCommand(int maxMass, Location destination)
        {
            MaxMass = maxMass;
            Destination = destination;
        }
    }
}