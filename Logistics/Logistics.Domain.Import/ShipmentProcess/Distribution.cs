﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Import.ShipmentProcess
{
    public class Distribution
    {
        private int ShipmentProcessId;
        private DistributionStatus StatusId;
        private Location Destination;        

        public Distribution(int shipmentProcessId, DistributionStatus statusId, Location destination)
        {
            ShipmentProcessId = shipmentProcessId;
            StatusId = statusId;
            Destination = destination;
        }
        internal void ShipmentArrivedOnTerminal(Location terminal, WarehouseReceivingStatus? warehouseReceivingStatus)
        {
            if (Destination == terminal && StatusId == DistributionStatus.Organized)
            {
                if(warehouseReceivingStatus == WarehouseReceivingStatus._203)
                {
                    StatusId = DistributionStatus.WarehouseReceiving;
                } else
                {
                    StatusId = DistributionStatus.OnTerminal;
                }
            }            
        }
    }
}