using System;
using System.Collections;

namespace Logistics.Domain.Import.ShipmentRoute
{
    public class TransportArrival
    {        
        internal TransportStatus TransportStatus {get; private set;}

        private ICollection<ShipmentRoute> shipmentRoutes;
        internal void ArrivedOnTerminal(TransportStatus transportStatus) 
        {
            TransportStatus = transportStatus;
            foreach(var shipmentRoute in shipmentRoutes) {
                
            }
        }
    }
}
