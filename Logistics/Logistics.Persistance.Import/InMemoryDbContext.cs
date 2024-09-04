using System;
using System.Collections.Generic;
using Logistics.Domain.Base;

namespace Logistics.Persistance.Import;

public static class InMemoryDbContext
{
    // Static list to hold data globally
    public static List<Aggregate> Aggregates { get; } = new List<Aggregate>();

    public static void SetupData()
    {
        // Add shipment routes
        Aggregates.Add(new Domain.Import.ShipmentRoute.ShipmentRoute(
            Guid.NewGuid(),
            10,
            new Domain.Import.Location("HR", "Zagreb", "10000", "Terminal 1"),
            new Domain.Import.Location("HR", "Zagreb", "10000", "Terminal 2"),
        1
            ));
        Aggregates.Add(new Domain.Import.ShipmentRoute.ShipmentRoute(
            Guid.NewGuid(),
            20,
            new Domain.Import.Location("HR", "Zagreb", "10000", "Terminal 1"),
            new Domain.Import.Location("HR", "Zagreb", "10000", "Terminal 3"),
            1
            ));

        Aggregates.Add(new Domain.Import.ShipmentRoute.Transport(1,100,0));

    }
}
