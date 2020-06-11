using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using DataAccess.Repositories;
using DataAccess.Entities;
using System.Threading.Tasks;

namespace DataAccess.Test
{
    public class GenericRepositoriesTest
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericRepository<Product> _repository;
        public GenericRepositoriesTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new GenericRepositories<Product>(_context);
        }
        [Fact]
        public void AddTest()
        {
            var totalCount = _context.Products.Count();
            var product1 = new Product()
            {
                Balances = new List<Balance>(),
                BarCode = "ghasvuiykhvbdsuagg65621356",
                BrandId = 1,
                Brands = new Brand(),
                Categories = new Category(),
                CategoriesId = 1,
                Id = 1,
                MinimumWholesaleOrder = 20,
                OrderLevel = 200,
                Price = 60,
                ProductName = "product1",
                ProductWholeSales = new List<ProductWholeSale>(),
                StockLevel = 80,
                WholesalePrice = 80
            };
            var product2 = new Product()
            {
                Balances = new List<Balance>(),
                BarCode = "scvahvfsiuohnkndvoij5451324654",
                BrandId = 1,
                Brands = new Brand(),
                Categories = new Category(),
                CategoriesId = 2,
                Id = 2,
                MinimumWholesaleOrder = 30,
                OrderLevel = 600,
                Price = 80,
                ProductName = "product2",
                ProductWholeSales = new List<ProductWholeSale>(),
                StockLevel = 80,
                WholesalePrice = 70
            };

            _repository.Add(product1);
            _repository.Add(product2);
            _context.SaveChanges();

            var productList = _context.Products.ToList();

            productList.Count.Should().Be(totalCount + 2);
            productList.Should().Contain(product1);
            productList.Should().Contain(product2);
        }

        [Fact]
        public void DeleteTest()
        {
            var product1 = new Product()
            {
                Balances = new List<Balance>(),
                BarCode = "ghasvuiykhvbdsuagg65621356",
                BrandId = 1,
                Brands = new Brand(),
                Categories = new Category(),
                CategoriesId = 1,
                Id = 1,
                MinimumWholesaleOrder = 20,
                OrderLevel = 200,
                Price = 60,
                ProductName = "product1",
                ProductWholeSales = new List<ProductWholeSale>(),
                StockLevel = 80,
                WholesalePrice = 80
            };
            var product2 = new Product()
            {
                Balances = new List<Balance>(),
                BarCode = "scvahvfsiuohnkndvoij5451324654",
                BrandId = 1,
                Brands = new Brand(),
                Categories = new Category(),
                CategoriesId = 2,
                Id = 2,
                MinimumWholesaleOrder = 30,
                OrderLevel = 600,
                Price = 80,
                ProductName = "product2",
                ProductWholeSales = new List<ProductWholeSale>(),
                StockLevel = 80,
                WholesalePrice = 70
            };
            _context.Add(product1);
            _context.Add(product2);
            _context.SaveChanges();

            _repository.Delete(product1);
            _context.SaveChanges();

            _context.Products.Count().Should().Be(1);
            _context.Set<Product>().ToList().First().Should().BeEquivalentTo(product2);

        }

        [Fact]
        public async Task FindAsyncTestAsync()
        {
            var product1 = new Product()
            {
                Balances = new List<Balance>(),
                BarCode = "ghasvuiykhvbdsuagg65621356",
                BrandId = 1,
                Brands = new Brand(),
                Categories = new Category(),
                CategoriesId = 1,
                Id = 1,
                MinimumWholesaleOrder = 20,
                OrderLevel = 200,
                Price = 60,
                ProductName = "product1",
                ProductWholeSales = new List<ProductWholeSale>(),
                StockLevel = 80,
                WholesalePrice = 80
            };
            var product2 = new Product()
            {
                Balances = new List<Balance>(),
                BarCode = "scvahvfsiuohnkndvoij5451324654",
                BrandId = 1,
                Brands = new Brand(),
                Categories = new Category(),
                CategoriesId = 2,
                Id = 2,
                MinimumWholesaleOrder = 30,
                OrderLevel = 600,
                Price = 80,
                ProductName = "product2",
                ProductWholeSales = new List<ProductWholeSale>(),
                StockLevel = 80,
                WholesalePrice = 70
            };
            _context.Add(product1);
            _context.Add(product2);
            _context.SaveChanges();

            var result = await _repository.FindAsync(e => e.ProductName == product1.ProductName);
            result.Should().BeEquivalentTo(product1);
        }

        [Fact]
        public void GetAllTest()
        {
            var product1 = new Product()
            {
                Balances = new List<Balance>(),
                BarCode = "ghasvuiykhvbdsuagg65621356",
                BrandId = 1,
                Brands = new Brand(),
                Categories = new Category(),
                CategoriesId = 1,
                Id = 1,
                MinimumWholesaleOrder = 20,
                OrderLevel = 200,
                Price = 60,
                ProductName = "product1",
                ProductWholeSales = new List<ProductWholeSale>(),
                StockLevel = 80,
                WholesalePrice = 80
            };
            var product2 = new Product()
            {
                Balances = new List<Balance>(),
                BarCode = "scvahvfsiuohnkndvoij5451324654",
                BrandId = 1,
                Brands = new Brand(),
                Categories = new Category(),
                CategoriesId = 2,
                Id = 2,
                MinimumWholesaleOrder = 30,
                OrderLevel = 600,
                Price = 80,
                ProductName = "product2",
                ProductWholeSales = new List<ProductWholeSale>(),
                StockLevel = 80,
                WholesalePrice = 70
            };
            _context.Add(product1);
            _context.Add(product2);
            _context.SaveChanges();

            var result = _repository.GetAll().ToList();

            result.Count().Should().Be(2);
            result.Should().Contain(product1);
            result.Should().Contain(product2);
        }

        [Fact]
        public void UpdateTest()
        {
            var product1 = new Product()
            {
                Balances = new List<Balance>(),
                BarCode = "ghasvuiykhvbdsuagg65621356",
                BrandId = 1,
                Brands = new Brand(),
                Categories = new Category(),
                CategoriesId = 1,
                Id = 1,
                MinimumWholesaleOrder = 20,
                OrderLevel = 200,
                Price = 60,
                ProductName = "product1",
                ProductWholeSales = new List<ProductWholeSale>(),
                StockLevel = 80,
                WholesalePrice = 80
            };
            _context.Add(product1);
            _context.SaveChanges();

            product1.ProductName = "product2";
            _repository.Update(product1);
            _context.SaveChanges();

            _context.Products.FirstOrDefault().ProductName.Should().Be("product2");
        }

        [Fact]
        public void SaveTest()
        {
            var product1 = new Product()
            {
                Balances = new List<Balance>(),
                BarCode = "ghasvuiykhvbdsuagg65621356",
                BrandId = 1,
                Brands = new Brand(),
                Categories = new Category(),
                CategoriesId = 1,
                Id = 1,
                MinimumWholesaleOrder = 20,
                OrderLevel = 200,
                Price = 60,
                ProductName = "product1",
                ProductWholeSales = new List<ProductWholeSale>(),
                StockLevel = 80,
                WholesalePrice = 80
            };
            _context.Add(product1);

            _repository.Save();

            _context.Products.ToList().Count().Should().Be(1);
        }

        [Fact]
        public async Task SaveAsyncTestAsync()
        {

            var product1 = new Product()
            {
                Balances = new List<Balance>(),
                BarCode = "ghasvuiykhvbdsuagg65621356",
                BrandId = 1,
                Brands = new Brand(),
                Categories = new Category(),
                CategoriesId = 1,
                Id = 1,
                MinimumWholesaleOrder = 20,
                OrderLevel = 200,
                Price = 60,
                ProductName = "product1",
                ProductWholeSales = new List<ProductWholeSale>(),
                StockLevel = 80,
                WholesalePrice = 80
            };
            _context.Add(product1);

            var result = await _repository.SaveAsync();

            _context.Products.ToList().Count().Should().Be(1);
        }
    }
}
