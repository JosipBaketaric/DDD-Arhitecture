using System;
using Logistics.Domain.Import.ShipmentRoute;

namespace Logistics.Application.Command.Import;

public class ChangeTransportStatusCommand
{
    public int TransportId {get; set;}
    public TransportStatus TransportStatus {get; set;}
}
