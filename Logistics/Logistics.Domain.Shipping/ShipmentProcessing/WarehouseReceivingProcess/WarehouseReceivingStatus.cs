using System.ComponentModel;

namespace Logistics.Domain.Shipping.ShipmentProcessing.WarehouseReceivingProcess
{
    public enum WarehouseReceivingStatus
    {
        [Description("201")]
        Entry,
        [Description("202")]
        Organized,
        [Description("203")]
        OnTerminal,
        [Description("204")]
        Loading,
        [Description("205")]
        InWarehouse,
    }
}
