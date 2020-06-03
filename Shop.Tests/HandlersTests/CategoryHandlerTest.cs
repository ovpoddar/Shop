using DataAccess.Entities;
using FluentAssertions;
using Moq;
using Shop.Handlers;
using Shop.Repositories;
using Shop.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class CategoryHandlerTest
    {
        private readonly Mock<IGenericRepository<Category>> _mock;
        private readonly CategoryHandler _categoryHandler;

        public CategoryHandlerTest()
        {
            _mock = new Mock<IGenericRepository<Category>>();
            _categoryHandler = new CategoryHandler(_mock.Object);
        }

        [Fact]
        public void GetId_FromString()
        {
            const int id = 2;
            _mock
                .Setup(e => e.GetAll())
                .Returns(new List<Category>
                {
                    new Category()
                    {
                        Id = id,
                        Name = "brand",
                        ParentId = 1
                    }

                }.AsQueryable());

            var result = _categoryHandler.GetId("brand");
            Assert.Equal(result, id);
        }

        [Fact]
        public void Categories_gettingall()
        {
            _mock
                .Setup(e => e.GetAll())
                .Returns(new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "drink",
                },
                new Category
                {
                    Id = 2,
                    Name = "drink",
                    ParentId = 1
                }
            }.AsQueryable());

            _categoryHandler.Categories().Should().BeEquivalentTo(new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "drink",
                }
            });
        }

        [Fact]
        public void GetAll_ReturnAll()
        {
            var expected = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "drink",
                },
                new Category
                {
                    Id = 2,
                    Name = "drink",
                    ParentId = 1
                }
            }.AsQueryable();

            _mock
                .Setup(e => e.GetAll())
                .Returns(expected);

            _categoryHandler.GetAll().Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(1, "drink1", false)]
        [InlineData(null, "drink8", true)]
        public void AddCategory_checkingsuccess(int? id, string name, bool expected)
        {
            var lists = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "drink",
                },
                new Category
                {
                    Id = 2,
                    Name = "drink1",
                    ParentId = 1
                }
            }.AsQueryable();

            _mock
                .Setup(e => e.GetAll())
                .Returns(lists);

            _mock
                .Setup(e => e.Add(It.IsAny<Category>()));

            var result = _categoryHandler.AddCategory(new CategoryViewModel
            {
                Id = id,
                Name = name
            });
            if (expected)
                Assert.True(result.Success);
            else
                Assert.False(result.Success);
        }
    }
}
