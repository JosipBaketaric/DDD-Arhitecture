using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Shipping.ShipmentProcessing.ImportProcess
{
    public enum ImportStatus
    {
        [Description("301")]
        Entry,
        [Description("302")]
        Organized,
        [Description("303")]
        OnTransport,
        [Description("304")]
        OnTerminal,
        [Description("305")]
        Delivered,

    }
}
