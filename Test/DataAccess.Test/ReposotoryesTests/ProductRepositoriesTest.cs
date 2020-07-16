using DataAccess.Entities;
using DataAccess.Helpers;
using DataAccess.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace DataAccess.Test.ReposotoryesTests
{
    public class ProductRepositoriesTest
    {
        private readonly Mock<ICatagoriesHelper> _catagoriesHelper;
        private readonly ProductRepositories _repositories;
        public ProductRepositoriesTest()
        {
            _catagoriesHelper = new Mock<ICatagoriesHelper>();
            _repositories = new ProductRepositories(_catagoriesHelper.Object);
        }
        [Fact]
        public void GetCategoryIds()
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
            _catagoriesHelper.Setup(e => e.Getvalues(It.IsAny<int>())).Verifiable();
            _catagoriesHelper.Setup(e => e.Categories).Returns(new List<Category>()
            {
                cateorie1, cateorie2,cateorie3
            });

            var result = _repositories.GetCategoryIds(1);
            result.Should().NotBeEmpty();
            result.Should().HaveCount(3);
        }
    }
}
