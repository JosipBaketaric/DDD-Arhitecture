using System;
using Logistics.Domain.Import;
using Logistics.Domain.Import.ShipmentRoute;

namespace Logistics.Application.Command.Import;

public class ChangeTransportStatusCommand
{
    public Guid TransportId {get; private set;}
    public TransportStatus TransportStatus {get; private set;}
    public Location StatusLocation { get; private set;}

    public ChangeTransportStatusCommand(Guid transportId, TransportStatus transportStatus, Location statusLocation)
    {
        TransportId = transportId;
        TransportStatus = transportStatus;
        StatusLocation = statusLocation;
    }
}
