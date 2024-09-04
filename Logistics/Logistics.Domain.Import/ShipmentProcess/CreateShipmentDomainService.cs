using Logistics.Domain.Base;
namespace Logistics.Domain.Import.ShipmentProcess
{
    public class CreateShipmentDomainService
    {
        public (Shipment, List<ShipmentRoute.ShipmentRoute>) CreateShipmentInImport(
            int mass,
            Location shipmentOrigin,
            Location? importDestination,
            Location shipmentDestination,
            bool usesWarehouse)
        {
            var shipment = new Shipment(mass, shipmentOrigin, shipmentDestination, importDestination, usesWarehouse);
            
            Console.WriteLine("Shipment created event handler called.");
            var shipmentRoutes = new List<ShipmentRoute.ShipmentRoute>();
            var routeSegmentIndex = 1;
            if (shipment.Import != null)
            {
                var shipmentRoute = new ShipmentRoute.ShipmentRoute(
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
                var shipmentRoute = new ShipmentRoute.ShipmentRoute(
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
