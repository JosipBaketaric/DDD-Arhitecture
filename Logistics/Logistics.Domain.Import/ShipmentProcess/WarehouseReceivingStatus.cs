using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Import.ShipmentProcess
{
    public enum WarehouseReceivingStatus
    {
        [Description("201")]
        Entry,
        [Description("202")]
        ReadyForLoading,
        [Description("203")]
        OnTerminal,
        [Description("204")]
        Loading,
        [Description("205")]
        InWarehouse,
    }
}
