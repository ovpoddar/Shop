using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Shop.Entities;
using Shop.Handlers;
using Shop.Handlers.Interfaces;
using Shop.Managers;
using Shop.Models;
using Xunit;

namespace Shop.Tests.HandlerTests
{
    public class PaymentHandlerTest
    {
        private readonly Mock<IRequestManger> _mock;
        private readonly Mock<IProductHandler> _mock1;
        private readonly PaymentHandler _productHandler;
        public PaymentHandlerTest()
        {
            _mock = new Mock<IRequestManger>();
            _mock1 = new Mock<IProductHandler>();
            _productHandler = new PaymentHandler(_mock.Object, _mock1.Object);
        }

        [Fact]
        public void GetProducts_test()
        {
            _mock1
                .Setup(e => e.GetProduct(It.IsAny<string>()))
                .Returns(Getsome());

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
                },
            };

            var result = _productHandler.GetProducts(input);

            input.Should().NotBeNull();
        }

        public Product Getsome() =>
            new Product
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
                    Id= It.IsAny<int>(),
                },
                Categories = new Category()
                {
                    Name = "yooy",
                    Id = It.IsAny<int>(),
                }
            };
    }
}
