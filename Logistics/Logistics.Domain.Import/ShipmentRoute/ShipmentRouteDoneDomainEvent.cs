using System;
using Logistics.Domain.Base;

namespace Logistics.Domain.Import.ShipmentRoute;

public class ShipmentRouteStatusChangeDomainEvent : IDomainEvent
{
    public Guid ShipmentId { get; private set; }
    public Guid ShipmentRouteId { get; private set; }
    public Location Terminal { get; private set; }
    public ShipmentProcessType ShipmentProcessType { get; private set; }
    public ShipmentRouteStatus Status { get; private set; }

    public ShipmentRouteStatusChangeDomainEvent(Guid shipmentId, Guid shipmentRouteId, Location terminal, ShipmentProcessType shipmentProcessType, ShipmentRouteStatus status)
    {
        ShipmentRouteId = shipmentRouteId;
        ShipmentId = shipmentId;
        Terminal = terminal;
        ShipmentProcessType = shipmentProcessType;
        Status = status;
    }
}
