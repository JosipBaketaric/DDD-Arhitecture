namespace Logistics.Domain.Import.ShipmentRoute;

public class TransportStatus
{
    internal int Status {get; private set;}
    internal Location Location {get; private set;}

    public TransportStatus(int Status, Location location) {
        Status = Status;
        Location = location;
    }
}
