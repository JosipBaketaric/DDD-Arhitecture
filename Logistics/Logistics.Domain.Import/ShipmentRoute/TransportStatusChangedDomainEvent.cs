using System;
using Logistics.Domain.Base;

namespace Logistics.Domain.Import.ShipmentRoute;

public class TransportStatusChangedDomainEvent: IDomainEvent
{
    public Guid TransportId { get; private set; }
    public TransportStatus TransportStatus {get; private set; }

    public Location Location { get; private set;}

    public TransportStatusChangedDomainEvent(Guid transportId, TransportStatus transportStatus, Location location)
    {
        TransportId = transportId;
        TransportStatus = transportStatus;
        Location = location;
    }
}
