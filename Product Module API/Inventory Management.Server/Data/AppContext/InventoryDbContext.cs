using Microsoft.EntityFrameworkCore;

namespace Inventory_Management.Server.Data
{
    public class InventoryDbContext : DbContext
    {

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; } 
    }
}
