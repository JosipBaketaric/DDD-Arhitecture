using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Import.ShipmentProcess
{
    public enum DistributionStatus
    {
        [Description("401")]
        EntryInProgress,
        [Description("402")]
        Organized,
        [Description("403")]
        WarehouseReceiving,
        [Description("404")]
        InWarehouse,
        [Description("405")]
        WarehouseDispatching,
        [Description("406")]
        OnTerminal,
        [Description("407")]
        OnTransport,
        [Description("408")]
        Delivered,
    }
}
