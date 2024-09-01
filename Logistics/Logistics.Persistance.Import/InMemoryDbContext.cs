using System;
using Logistics.Domain.Base;

namespace Logistics.Persistance.Import;

public static class InMemoryDbContext
{
    // Static list to hold data globally
    public static List<Aggregate> Aggregates { get; } = new List<Aggregate>();

    // Static method to add data to the list
    public static void AddData(Aggregate item)
    {
        Aggregates.Add(item);
    }


}
