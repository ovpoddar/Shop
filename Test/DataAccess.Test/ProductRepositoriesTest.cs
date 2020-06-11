using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;

namespace DataAccess.Test
{
    public class ProductRepositoriesTest
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductRepositories _repositories;
        public ProductRepositoriesTest()
        {
            var option = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new ApplicationDbContext(option);
            _repositories = new ProductRepositories(_context);
        }
        [Fact]
        public void GetCategoryIds()
        {
            var cateorie1 = new Category()
            {
                Id = 1,
                Name = "cateorie1",
                Parent = new Category(),
                Products = new List<Product>(),
                SubCategories = new List<Category>()
            };
            var cateorie2 = new Category()
            {
                Id = 2,
                Name = "cateorie2",
                Parent = new Category(),
                Products = new List<Product>(),
                SubCategories = new List<Category>(),
                ParentId = 0
            };
            _context.Add(cateorie1);
            _context.Add(cateorie2);
            _context.SaveChanges();

            var result = _repositories.GetCategoryIds(1);
        }
    }
}
