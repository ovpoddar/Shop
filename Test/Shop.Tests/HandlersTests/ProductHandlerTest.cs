//using DataAccess.Entities;
//using DataAccess.Repositories;
//using FluentAssertions;
//using Moq;
//using Shop.Handlers;
//using Shop.Models;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using Xunit;

//namespace Shop.Tests.HandlersTests
//{
//    public class ProductHandlerTest
//    {
//        private readonly Mock<IGenericRepository<Product>> _mock;
//        private readonly Mock<IProductRepositories> _mock1;
//        private readonly ProductHandler _productHandler;

//        public ProductHandlerTest()
//        {
//            _mock = new Mock<IGenericRepository<Product>>();
//            _mock1 = new Mock<IProductRepositories>();
//            _productHandler = new ProductHandler(_mock.Object, _mock1.Object);
//        }

//        [Fact]
//        public void AddProduct_true()
//        {
//            _mock
//                .Setup(e => e.GetAll())
//                .Returns(Getsome());

//            _mock
//                .Setup(e => e.Delete(It.IsAny<Product>()));

//            _mock
//                .Setup(e => e.Add(It.IsAny<Product>()));

//            _mock
//                .Setup(e => e.Save());

//            var result = _productHandler.AddProduct(new Product()
//            {
//                Id = It.IsAny<int>(),
//                ProductName = "hammer",
//                Price = (decimal)3.99,
//                StockLevel = 50,
//                BarCode = It.IsAny<string>(),
//                BrandId = It.IsAny<int>(),
//                CategoriesId = It.IsAny<int>(),
//                MinimumWholesaleOrder = It.IsAny<double>(),
//                OrderLevel = It.IsAny<double>(),
//                WholesalePrice = It.IsAny<double>()
//            });

//            Assert.True(result);
//        }

//        [Fact]
//        public void AddProduct_false()
//        {
//            _mock
//                .Setup(e => e.GetAll())
//                .Returns(Getsome());

//            _mock
//                .Setup(e => e.Delete(It.IsAny<Product>()));

//            _mock
//                .Setup(e => e.Add(It.IsAny<Product>()));

//            _mock
//                .Setup(e => e.Save());

//            var result = _productHandler.AddProduct(new Product()
//            {
//                Id = It.IsAny<int>(),
//                ProductName = "cola",
//                Price = (decimal).75,
//                StockLevel = 50,
//                BarCode = It.IsAny<string>(),
//                BrandId = It.IsAny<int>(),
//                CategoriesId = It.IsAny<int>(),
//                MinimumWholesaleOrder = It.IsAny<double>(),
//                OrderLevel = It.IsAny<double>(),
//                WholesalePrice = It.IsAny<double>()
//            });

//            Assert.False(result);
//        }

//        [Fact]
//        public void GetProduct_name_test()
//        {
//            _mock
//                .Setup(e => e.GetAll())
//                .Returns(Getsome());

//            var result = _productHandler.GetProduct("cola").Result;

//            result.Should().BeEquivalentTo(Getsome().ToList()[0]);
//        }

//        [Fact]
//        public void Products_test()
//        {
//            _mock
//               .Setup(e => e.GetAll())
//               .Returns(Getsome());

//            var result = _productHandler.Products(0);

//            result.Should().BeEquivalentTo(Getsome().ToList());
//        }

//        [Fact]
//        public void Products_test_with_id()
//        {
//            _mock
//               .Setup(e => e.GetAll())
//               .Returns(Getsome());

//            var result = _productHandler.Products(1, 0);

//            Assert.NotNull(result);
//        }

//        [Fact]
//        public void RemoveProduct_test_with_id()
//        {
//            _mock
//               .Setup(e => e.GetAll())
//               .Returns(Getsome());

//            _mock
//                .Setup(e => e.Update(It.IsAny<Product>()));


//            var result = _productHandler.RemoveProduct(new SaleProduct()
//            {
//                BarCode = It.IsAny<string>(),
//                Brand = It.IsAny<string>(),
//                BrandId = It.IsAny<int>(),
//                CategoriesId = It.IsAny<int>(),
//                Category = It.IsAny<string>(),
//                OrderLevel = It.IsAny<double>(),
//                Price = It.IsAny<decimal>(),
//                ProductId = 1,
//                ProductName = "cola",
//                SaleQuantity = 20
//            });
//            result.Success.Should().BeTrue();
//            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
//        }


//        [Fact]
//        public void RemoveProduct_test_with_id_fail()
//        {
//            _mock
//               .Setup(e => e.GetAll())
//               .Returns(Getsome());

//            _mock
//                .Setup(e => e.Update(It.IsAny<Product>()));


//            var result = _productHandler.RemoveProduct(new SaleProduct()
//            {
//                BarCode = It.IsAny<string>(),
//                Brand = It.IsAny<string>(),
//                BrandId = It.IsAny<int>(),
//                CategoriesId = It.IsAny<int>(),
//                Category = It.IsAny<string>(),
//                OrderLevel = It.IsAny<double>(),
//                Price = It.IsAny<decimal>(),
//                ProductId = 50,
//                ProductName = "mell",
//                SaleQuantity = 20
//            });

//            result.Exception.Should().Be("Value cannot be null.");
//            result.Result.Message.Should().Be("Product does not exist");
//        }

//        [Fact]
//        public void TotalCount_test()
//        {
//            _mock
//               .Setup(e => e.GetAll())
//               .Returns(Getsome());

//            var result = _productHandler.TotalCount();

//            Assert.Equal(1, result);
//        }

//        [Fact]
//        public void TotalCount_test_2()
//        {
//            _mock
//               .Setup(e => e.GetAll())
//               .Returns(Getsome());

//            var result = _productHandler.TotalCount(7);

//            Assert.Equal(0, result);
//        }

//        public IQueryable<Product> Getsome() =>
//            new List<Product>
//            {
//                new Product
//                {
//                    Id = 1,
//                    ProductName = "cola",
//                    Price = (decimal).75,
//                    StockLevel = 50,
//                    BarCode = It.IsAny<string>(),
//                    BrandId = It.IsAny<int>(),
//                    CategoriesId = It.IsAny<int>(),
//                    MinimumWholesaleOrder = It.IsAny<double>(),
//                    OrderLevel= It.IsAny<double>(),
//                    WholesalePrice= It.IsAny<double>()
//                },
//                new Product
//                {
//                    Id = 2,
//                    ProductName = "knife",
//                    Price = 1,
//                    StockLevel = 50,
//                    BarCode = It.IsAny<string>(),
//                    BrandId = It.IsAny<int>(),
//                    CategoriesId = It.IsAny<int>(),
//                    MinimumWholesaleOrder = It.IsAny<double>(),
//                    OrderLevel= It.IsAny<double>(),
//                    WholesalePrice= It.IsAny<double>()
//                },
//                new Product
//                {
//                    Id = 3,
//                    ProductName = "lease",
//                    Price = (decimal).05,
//                    StockLevel = 50,
//                    BarCode = It.IsAny<string>(),
//                    BrandId = It.IsAny<int>(),
//                    CategoriesId = It.IsAny<int>(),
//                    MinimumWholesaleOrder = It.IsAny<double>(),
//                    OrderLevel= It.IsAny<double>(),
//                    WholesalePrice= It.IsAny<double>()
//                },
//                new Product
//                {
//                    Id = 4,
//                    ProductName = "lease",
//                    Price = (decimal).05,
//                    StockLevel = 50,
//                    BarCode = It.IsAny<string>(),
//                    BrandId = It.IsAny<int>(),
//                    CategoriesId = It.IsAny<int>(),
//                    MinimumWholesaleOrder = It.IsAny<double>(),
//                    OrderLevel= It.IsAny<double>(),
//                    WholesalePrice= It.IsAny<double>()
//                },
//                new Product
//                {
//                    Id = 5,
//                    ProductName = "lease",
//                    Price = (decimal).05,
//                    StockLevel = 50,
//                    BarCode = It.IsAny<string>(),
//                    BrandId = It.IsAny<int>(),
//                    CategoriesId = It.IsAny<int>(),
//                    MinimumWholesaleOrder = It.IsAny<double>(),
//                    OrderLevel= It.IsAny<double>(),
//                    WholesalePrice= It.IsAny<double>()
//                },
//                new Product
//                {
//                    Id = 6,
//                    ProductName = "lease",
//                    Price = (decimal).05,
//                    StockLevel = 50,
//                    BarCode = It.IsAny<string>(),
//                    BrandId = It.IsAny<int>(),
//                    CategoriesId = It.IsAny<int>(),
//                    MinimumWholesaleOrder = It.IsAny<double>(),
//                    OrderLevel= It.IsAny<double>(),
//                    WholesalePrice= It.IsAny<double>()
//                },
//                new Product
//                {
//                    Id = 7,
//                    ProductName = "lease",
//                    Price = (decimal).05,
//                    StockLevel = 50,
//                    BarCode = It.IsAny<string>(),
//                    BrandId = It.IsAny<int>(),
//                    CategoriesId = It.IsAny<int>(),
//                    MinimumWholesaleOrder = It.IsAny<double>(),
//                    OrderLevel= It.IsAny<double>(),
//                    WholesalePrice= It.IsAny<double>()
//                },
//                new Product
//                {
//                    Id = 8,
//                    ProductName = "lease",
//                    Price = (decimal).05,
//                    StockLevel = 50,
//                    BarCode = It.IsAny<string>(),
//                    BrandId = It.IsAny<int>(),
//                    CategoriesId = It.IsAny<int>(),
//                    MinimumWholesaleOrder = It.IsAny<double>(),
//                    OrderLevel= It.IsAny<double>(),
//                    WholesalePrice= It.IsAny<double>()
//                }
//            }.AsQueryable();
//    }
//}
