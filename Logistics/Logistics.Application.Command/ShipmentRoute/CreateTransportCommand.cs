using Logistics.Domain.Shipping;

namespace Logistics.Application.Command.Shipping.ShipmentRoute
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