using DataAccess.Entities;
using DataAccess.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccess.Test.HelperTests
{
    public class CatagoriesHelperTest
    {
        private readonly ApplicationDbContext _context;
        private readonly ICatagoriesHelper _catagoriesHelper;
        public CatagoriesHelperTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new ApplicationDbContext(options);
            _catagoriesHelper = new CatagoriesHelper(_context);
        }
        [Fact]
        public void GetValuesTest()
        {
            var cateorie1 = new Category()
            {
                Id = 1,
                Name = "cateorie1",
            };
            var cateorie2 = new Category()
            {
                Id = 2,
                Name = "cateorie2",
                ParentId = 1
            };
            var cateorie3 = new Category()
            {
                Id = 3,
                Name = "cateorie3",
                ParentId = 2
            };
            _context.Categories.Add(cateorie1);
            _context.Categories.Add(cateorie2);
            _context.Categories.Add(cateorie3);
            _context.SaveChanges();


            _catagoriesHelper.Getvalues(1);

            _catagoriesHelper.Categories.Should().NotBeEmpty();
            _catagoriesHelper.Categories.Should().HaveCount(3);
        }
    }
}
