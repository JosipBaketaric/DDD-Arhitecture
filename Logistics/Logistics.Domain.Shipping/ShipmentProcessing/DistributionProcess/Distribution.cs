using Logistics.Domain.Base;
using Logistics.Domain.Shipping.ShipmentProcessing.WarehouseReceivingProcess;

namespace Logistics.Domain.Shipping.ShipmentProcessing.DistributionProcess
{
    public class Distribution: Entity
    {
        private Guid ShipmentProcessId;
        private Guid ShipmentId;
        private DistributionStatus StatusId;
        internal Location Origin { get; }
        internal Location Destination { get; }

        public Distribution(Guid shipmentProcessId, DistributionStatus statusId, Location origin, Location destination, Guid shipmentId)
        {
            ShipmentProcessId = shipmentProcessId;
            StatusId = statusId;
            Origin = origin;
            Destination = destination;
            ShipmentId = shipmentId;
        }
        internal void ShipmentArrivedOnTerminal(Location terminal, WarehouseReceivingStatus? warehouseReceivingStatus)
        {
            if (Origin == terminal && StatusId == DistributionStatus.Organized)
            {
                if (warehouseReceivingStatus == WarehouseReceivingStatus.Organized)
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
            if(status == DistributionStatus.Organized)
            {
                RaiseDomainEvent(new ShipmentDistributionOrganizedDomainEvent(ShipmentProcessId, Origin, Destination, ShipmentId));
            }
        }
    }
}
