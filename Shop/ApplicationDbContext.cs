using Microsoft.EntityFrameworkCore;
using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductWholeSale> ProductWholeSales { get; set; }
        public DbSet<Shop.Entities.WholesaleSize> WholesaleSize { get; set; }

    }
}
