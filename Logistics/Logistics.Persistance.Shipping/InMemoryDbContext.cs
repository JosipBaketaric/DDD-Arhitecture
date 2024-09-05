using System;
using System.Collections.Generic;
using Logistics.Domain.Base;

namespace Logistics.Persistance.Shipping;

public static class InMemoryDbContext
{
    // Static list to hold data globally
    public static List<Aggregate> Aggregates { get; } = new List<Aggregate>();    
}
