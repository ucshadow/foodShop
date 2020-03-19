using FoodStore.Entities;
using System.Data.Entity;

namespace FoodStore.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> PurchaseHistory { get; set; }
    }
}
