using Microsoft.EntityFrameworkCore;
using Shop.Entities;

namespace Shop
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductWholeSale> ProductWholeSales { get; set; }
        public DbSet<WholesaleSize> WholesaleSize { get; set; }

    }
}
