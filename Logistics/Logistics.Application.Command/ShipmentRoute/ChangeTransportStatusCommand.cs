using System;
using Logistics.Domain.Shipping;
using Logistics.Domain.Shipping.ShipmentRouting.Transporting;

namespace Logistics.Application.Command.Shipping.ShipmentRoute;

public class ChangeTransportStatusCommand
{
    public Guid TransportId { get; private set; }
    public TransportStatus TransportStatus { get; private set; }
    public Location StatusLocation { get; private set; }

    public ChangeTransportStatusCommand(Guid transportId, TransportStatus transportStatus, Location statusLocation)
    {
        TransportId = transportId;
        TransportStatus = transportStatus;
        StatusLocation = statusLocation;
    }
}
