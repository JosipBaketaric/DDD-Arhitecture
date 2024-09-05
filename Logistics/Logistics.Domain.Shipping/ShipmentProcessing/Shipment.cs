using Logistics.Domain.Base;
using Logistics.Domain.Shipping.ShipmentProcessing.DistributionProcess;
using Logistics.Domain.Shipping.ShipmentProcessing.ImportProcess;
using Logistics.Domain.Shipping.ShipmentProcessing.WarehouseReceivingProcess;

namespace Logistics.Domain.Shipping.ShipmentProcessing
{
    public class Shipment : Aggregate
    {
        public Guid ShipmentId { get; private set; }
        public Import Import { get; private set; }
        internal WarehouseReceiving WarehouseReceiving { get; private set; }
        public Distribution Distribution { get; private set; }
        public int Mass { get; private set; }
        public Shipment(Guid shipmentId,
            int mass,
            Location ShipmentOrigin,
            Location shipmentDestination,
            Guid? importProcessId,
            ImportStatus? importStatus,
            Location? importDestination,
            Guid? warehouseReceivingProcessId,
            WarehouseReceivingStatus? warehouseReceivingStatus,
            Location? warehouseTerminal,
            Guid? distributionProcessId,
            DistributionStatus? distributionStatus,
            Location? distributionOriginTerminal,
            Location? distributionDestinationTerminal)
        {
            Construct(shipmentId, mass, ShipmentOrigin, shipmentDestination, importProcessId, importStatus, importDestination, warehouseReceivingProcessId, warehouseReceivingStatus, warehouseTerminal, distributionProcessId, distributionStatus, distributionOriginTerminal, distributionDestinationTerminal);
        }

        private void Construct(Guid shipmentId,
            int mass,
            Location ShipmentOrigin,
            Location shipmentDestination,
            Guid? importProcessId,
            ImportStatus? importStatus,
            Location? importDestination,
            Guid? warehouseReceivingProcessId,
            WarehouseReceivingStatus? warehouseReceivingStatus,
            Location? warehouseTerminal,
            Guid? distributionProcessId,
            DistributionStatus? distributionStatus,
            Location? distributionOriginTerminal,
            Location? distributionDestinationTerminal)
        {
            ShipmentId = shipmentId;
            Mass = mass;
            if (importProcessId.HasValue)
            {
                Import = new Import(importProcessId.Value, shipmentId, importStatus.Value, ShipmentOrigin, importDestination);
            }
            if (warehouseReceivingProcessId.HasValue)
            {
                WarehouseReceiving = new WarehouseReceiving(warehouseReceivingProcessId.Value, warehouseReceivingStatus.Value, warehouseTerminal, shipmentId);
            }
            if (distributionProcessId.HasValue)
            {
                Distribution = new Distribution(distributionProcessId.Value, distributionStatus.Value, distributionOriginTerminal, distributionDestinationTerminal, shipmentId);
            }
        }

        internal Shipment(
            int mass,
            Location shipmentOrigin,
            Location? importDestination,
            Location shipmentDestination,
            bool usesWarehouse
        )
        {
            bool hasDistribution = false;
            if (shipmentDestination.Country == "HR")
            {
                hasDistribution = true;
            }

            Construct(
                Guid.NewGuid(),
                mass,
                shipmentOrigin,
                shipmentDestination,
                Guid.NewGuid(),
                ImportStatus.Entry,
                importDestination,
                usesWarehouse ? Guid.NewGuid() : null,
                usesWarehouse ? WarehouseReceivingStatus.Entry : null,
                usesWarehouse ? importDestination : null,
                hasDistribution ? Guid.NewGuid() : null,
                hasDistribution ? DistributionStatus.EntryInProgress : null,
                hasDistribution ? importDestination : null,
                hasDistribution ? shipmentDestination : null);

            if (Import != null)
            {
                Console.WriteLine("Import process started");
            }
            if (WarehouseReceiving != null)
            {
                Console.WriteLine("Warehouse receiving process started");
            }
            if (Distribution != null)
            {
                Console.WriteLine("Distribution process started");
            }

            RaiseDomainEvent(new ShipmentCreatedDomainEvent(this));
        }
        public void ImportStatusChange(ImportStatus importStatus, Location location)
        {
            if (Import == null)
            {
                throw new InvalidOperationException("Import process is not started");
            }

            if (importStatus == ImportStatus.OnTerminal)
            {
                Import.ShipmentArrivedOnTerminal(location);
                if (WarehouseReceiving != null)
                {
                    WarehouseReceiving.ShipmentArrivedOnTerminal(location);
                }
                if (Distribution != null)
                {
                    Distribution.ShipmentArrivedOnTerminal(location, WarehouseReceiving?.StatusId);
                }
            }
            else
            {
                Import.StatusChanged(importStatus);
            }
        }

        public void WarehouseReceivingStatusChange(WarehouseReceivingStatus warehouseReceivingStatus, Location location)
        {
            if (WarehouseReceiving == null)
            {
                throw new InvalidOperationException("Warehouse receiving process is not started");
            }

            if (warehouseReceivingStatus == WarehouseReceivingStatus.OnTerminal)
            {
                WarehouseReceiving.ShipmentArrivedOnTerminal(location);
                if (Distribution != null)
                {
                    Distribution.ShipmentArrivedOnTerminal(location, warehouseReceivingStatus);
                }
            }
            else
            {
                WarehouseReceiving.StatusChanged(warehouseReceivingStatus);
            }
        }

        public void DistributionStatusChange(DistributionStatus distributionStatus, Location location)
        {
            if (Distribution == null)
            {
                throw new InvalidOperationException("Distribution process is not started");
            }

            if (distributionStatus == DistributionStatus.OnTerminal)
            {
                Distribution.ShipmentArrivedOnTerminal(location, WarehouseReceiving?.StatusId);
            }
            else
            {
                Distribution.StatusChanged(distributionStatus);
            }
        }
    }
}
