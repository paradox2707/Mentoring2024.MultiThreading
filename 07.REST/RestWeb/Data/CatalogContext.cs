using Microsoft.EntityFrameworkCore;
using RestWeb.Models;

namespace RestWeb.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Books" },
                new Category { Id = 3, Name = "Clothing" }
            );

            // Seed data for Items
            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Laptop", Description = "A personal computer for mobile use.", CategoryId = 1 },
                new Item { Id = 2, Name = "Smartphone", Description = "A portable device that combines mobile telephone and computing functions.", CategoryId = 1 },
                new Item { Id = 3, Name = "Novel", Description = "A long narrative work of fiction.", CategoryId = 2 },
                new Item { Id = 4, Name = "T-Shirt", Description = "A casual top worn by men and women.", CategoryId = 3 }
            );
        }
    }
}
