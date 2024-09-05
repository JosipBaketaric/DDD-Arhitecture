using System.ComponentModel;

namespace Logistics.Domain.Shipping.ShipmentRouting.Transporting
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
