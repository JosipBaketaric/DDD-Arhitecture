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
        [Description("Entry in progress")]
        _201,
        [Description("Waiting")]
        _202,
        [Description("On terminal")]
        _203,
        [Description("Loading")]
        _204,
        [Description("In warehouse")]
        _205,
    }
}
