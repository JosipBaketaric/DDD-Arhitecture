using System;
using Logistics.Domain.Import;

namespace Logistics.Application.Command.Import;

public class ChangeTransportStatusCommand
{
    public int TransportId {get; private set;}
    public int TransportStatus {get; private set;}
    public Location Location { get; private set;}

    public ChangeTransportStatusCommand(int transportId, int transportStatus, Location location)
    {
        TransportId = transportId;
        TransportStatus = transportStatus;
        Location = location;
    }

}
