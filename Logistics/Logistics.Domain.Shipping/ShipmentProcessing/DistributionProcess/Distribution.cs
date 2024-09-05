using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistics.Domain.Shipping.ShipmentProcessing.WarehouseReceivingProcess;

namespace Logistics.Domain.Shipping.ShipmentProcessing.DistributionProcess
{
    public class Distribution
    {
        private Guid ShipmentProcessId;
        private DistributionStatus StatusId;
        internal Location Origin { get; }
        internal Location Destination { get; }

        public Distribution(Guid shipmentProcessId, DistributionStatus statusId, Location origin, Location destination)
        {
            ShipmentProcessId = shipmentProcessId;
            StatusId = statusId;
            Origin = origin;
            Destination = destination;
        }
        internal void ShipmentArrivedOnTerminal(Location terminal, WarehouseReceivingStatus? warehouseReceivingStatus)
        {
            if (Origin == terminal && StatusId == DistributionStatus.Organized)
            {
                if (warehouseReceivingStatus == WarehouseReceivingStatus.ReadyForLoading)
                {
                    StatusId = DistributionStatus.WarehouseReceiving;
                }
                else
                {
                    StatusId = DistributionStatus.OnTerminal;
                }
                Console.WriteLine("Distribution Status changed to " + StatusId);
            }
        }

        internal void StatusChanged(DistributionStatus status)
        {
            if (status == DistributionStatus.OnTerminal)
            {
                throw new InvalidOperationException("Cannot change status to OnTerminal directly");
            }
            StatusId = status;
            Console.WriteLine("Distribution Status changed to " + StatusId);
        }
    }
}
