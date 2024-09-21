using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Logistics.Persistance.Shipping
{
    public class ShippingDbContext: DbContext
    {
        public ShippingDbContext(DbContextOptions<ShippingDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}
