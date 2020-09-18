using DataAccess.Entities;
using FluentAssertions;
using Moq;
using Shop.Handlers;
using Shop.Handlers.Interfaces;
using Shop.Models;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace shop.tests.handlerstests
{
    public class paymenthandlertest
    {
        private readonly Mock<IProductHandler> _mock1;
        private readonly PaymentHandler _producthandler;
        public paymenthandlertest()
        {
            _mock1 = new Mock<IProductHandler>();
            _producthandler = new PaymentHandler(_mock1.Object);
        }

        [Fact]
        public void getproducts_test()
        {
            _mock1
                .Setup(e => e.GetProduct(It.IsAny<string>()))
                .Returns(getsome())
                .Verifiable();

            var input = new List<ItemModel>()
            {
                new ItemModel()
                {
                    Id = 1,
                    Brand = It.IsAny<string>(),
                    Name ="cola",
                    Price = (decimal).75,
                    Quantity =20,
                    TotalPrice = 100
                }
            };

            var result = _producthandler.GetProducts(input);

            input.Should().NotBeNull();
        }

        private Results<Product> getsome() =>
            new Results<Product>()
            {
                Exception = null,
                HttpStatusCode = HttpStatusCode.OK,
                Result = new Product
                {
                    Id = 1,
                    ProductName = "cola",
                    Price = (decimal).75,
                    StockLevel = 150,
                    BarCode = "sachbasdhb654612",
                    BrandId = 2,
                    CategoriesId = 1,
                    MinimumWholesaleOrder = 20,
                    OrderLevel = 80,
                    WholesalePrice = 20,
                    Brands = new Brand()
                    {
                        BrandName = "xoxo",
                        Id = It.IsAny<int>(),
                    },
                    Categories = new Category()
                    {
                        Name = "yooy",
                        Id = It.IsAny<int>(),
                    }
                },
                Success = true
            };
    }
}
