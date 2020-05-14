using Moq;
using Shop.Entities;
using Shop.Handlers;
using Shop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace test_for_all
{
    public class BrandHandlerTest
    {
        private readonly Mock<IGenericRepository<Brand>> _mock;
        private readonly BrandHandler _brandHandler;
        public BrandHandlerTest()
        {
            _mock = new Mock<IGenericRepository<Brand>>() ?? throw new ArgumentNullException(nameof(_mock));
            _brandHandler = new BrandHandler(_mock.Object) ?? throw new ArgumentNullException(nameof(_brandHandler));
        }
        [Theory]
        [InlineData("brand", "brand", false)]
        [InlineData("brand", "brand1", true)]
        public void GetLastBalance_Balance(string existingName, string addingname, bool type)
        {
            _mock
                .Setup(_ => _.GetAll())
                .Returns(new List<Brand>
                {
                    new Brand()
                    {
                        Id = 1,
                        BrandName = existingName
                    }

                }.AsQueryable());

            _mock
                .Setup(_ => _.Add(It.IsAny<Brand>()));

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
                .Setup(_ => _.GetAll())
                .Returns(new List<Brand>
                {
                    new Brand()
                    {
                        Id = 1,
                        BrandName = existingName
                    }

                }.AsQueryable());

            _mock
                .Setup(_ => _.Add(It.IsAny<Brand>()));

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
                .Setup(_ => _.GetAll())
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
