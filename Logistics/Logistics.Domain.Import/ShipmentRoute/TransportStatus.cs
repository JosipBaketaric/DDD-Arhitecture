using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Import.ShipmentRoute
{
    public enum TransportStatus
    {
        [Description("301")]
        Entry,
        [Description("302")]
        Organized,
        [Description("303")]
        Driving,
        [Description("304")]
        OnTerminal,
        [Description("305")]
        Done
    }
}
