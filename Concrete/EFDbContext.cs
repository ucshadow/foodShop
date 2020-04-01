using FoodStore.Entities;
using System.Data.Entity;

namespace FoodStore.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> PurchaseHistory { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PublicProfile> PublicProfiles { get; set; }
        public DbSet<Affiliate> Affiliates { get; set; }
        //public DbSet<AffiliateSell> AffiliateSells { get; set; }
    }
}
