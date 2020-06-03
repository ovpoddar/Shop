﻿using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Shop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductWholeSale> ProductWholeSales { get; set; }
        public DbSet<WholesaleSize> WholesaleSize { get; set; }
        public DbSet<Csv> Csvs { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Employer> Employers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BalanceDataConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTypeDataConfiguration());
        }
    }
}
