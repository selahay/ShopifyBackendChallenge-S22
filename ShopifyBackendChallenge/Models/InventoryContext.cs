using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopifyBackendChallenge.Models
{
    public class InventoryContext: DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

        public DbSet<Inventory> Inventory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Inventory>().HasData(
                new Inventory
                {
                    ItemId = 1,
                    ItemName = "Sneakers",
                    ItemDescription = "Women's. Red. White sole.",
                    InStock = true,
                    IsDeleted = false
                }
                );
        }
    }
}
