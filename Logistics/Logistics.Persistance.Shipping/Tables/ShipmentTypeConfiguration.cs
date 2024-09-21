using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
namespace Logistics.Persistance.Shipping.Tables
{
    public class Shipment
    {
        public Guid Id { get; set; }
        public int Mass { get; set; }
    }


    public class ShipmentTypeConfiguration :IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.HasKey(u => u.Id);            
        }
    }
}
