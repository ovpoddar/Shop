using DataAccess.Entities;
using Moq;
using Shop.Handlers;
using Shop.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class BrandHandlerTest
    {
        private readonly Mock<IGenericRepository<Brand>> _mock;
        private readonly BrandHandler _brandHandler;
        public BrandHandlerTest()
        {
            _mock = new Mock<IGenericRepository<Brand>>();
            _brandHandler = new BrandHandler(_mock.Object);
        }

        [Theory]
        [InlineData("brand", "brand", false)]
        [InlineData("brand", "brand1", true)]
        public void GetLastBalance_Balance(string existingName, string addingname, bool type)
        {
            _mock
                .Setup(e => e.GetAll())
                .Returns(new List<Brand>
                {
                    new Brand()
                    {
                        Id = 1,
                        BrandName = existingName
                    }

                }.AsQueryable());

            _mock
                .Setup(e => e.Add(It.IsAny<Brand>()));

            if (type)
                Assert.True(_brandHandler.AddBrand(new Brand()
                {
                    Id = 1,
                    BrandName = addingname
                }));
            else
                Assert.False(_brandHandler.AddBrand(new Brand()
                {
                    Id = 2,
                    BrandName = addingname
                }));
        }

        [Theory]
        [InlineData("brand", "brand", "brand")]
        [InlineData("brand", "brand1", "brand1")]
        public void AddBrandWithReturn_Balance(string existingName, string addingname, string returningbrand)
        {
            _mock
                .Setup(e => e.GetAll())
                .Returns(new List<Brand>
                {
                    new Brand()
                    {
                        Id = 1,
                        BrandName = existingName
                    }

                }.AsQueryable());

            _mock
                .Setup(e => e.Add(It.IsAny<Brand>()));

            var result = _brandHandler.AddBrandWithReturn(new Brand
            {
                BrandName = addingname,
                Id = 2
            });
            Assert.Equal(result.BrandName, returningbrand);
        }

        [Fact]
        public void GetId_FromString()
        {
            var id = 1;
            _mock
                .Setup(e => e.GetAll())
                .Returns(new List<Brand>
                {
                    new Brand()
                    {
                        Id = id,
                        BrandName = "brand"
                    }

                }.AsQueryable());
            var result = _brandHandler.GetId("brand");
            Assert.Equal(result, id);
        }
    }
}
