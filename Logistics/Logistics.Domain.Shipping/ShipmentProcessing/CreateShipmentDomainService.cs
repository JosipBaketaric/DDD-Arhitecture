using Logistics.Domain.Shipping.ShipmentRouting;

namespace Logistics.Domain.Shipping.ShipmentProcessing
{
    public class CreateShipmentDomainService
    {
        public (Shipment, List<ShipmentRouting.ShipmentRoute>) CreateShipmentInImport(
            int mass,
            Location shipmentOrigin,
            Location? importDestination,
            Location shipmentDestination,
            bool usesWarehouse)
        {
            var shipment = new Shipment(mass, shipmentOrigin, shipmentDestination, importDestination, usesWarehouse);

            Console.WriteLine("Shipment created");
            var shipmentRoutes = new List<ShipmentRoute>();
            var routeSegmentIndex = 1;
            if (shipment.Import != null)
            {
                var shipmentRoute = new ShipmentRoute(
                    shipment.ShipmentId,
                    shipment.Mass,
                    shipment.Import.Origin,
                    shipment.Import.Destination,
                    routeSegmentIndex,
                    ShipmentProcessType.Import);
                Console.WriteLine("Import route created");
                routeSegmentIndex++;
                shipmentRoutes.Add(shipmentRoute);
            }
            if (shipment.Distribution != null)
            {
                var shipmentRoute = new ShipmentRoute(
                    shipment.ShipmentId,
                    shipment.Mass,
                    shipment.Distribution.Origin,
                    shipment.Distribution.Destination,
                    routeSegmentIndex,
                    ShipmentProcessType.Distribution);
                Console.WriteLine("Distribution route created");
                shipmentRoutes.Add(shipmentRoute);
            }

            return (shipment, shipmentRoutes);
        }
    }
}
