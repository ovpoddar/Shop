using DataAccess.Entities;
using FluentAssertions;
using Moq;
using Shop.Handlers;
using Shop.Handlers.Interfaces;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class ItemHandlerTest
    {
        private readonly Mock<IProductHandler> _productHandler;
        private readonly ItemHandler _itemHandler;
        public ItemHandlerTest()
        {
            _productHandler = new Mock<IProductHandler>();
            _itemHandler = new ItemHandler(_productHandler.Object);
        }

        [Fact]
        public void AddItemModelValidationTest()
        {
            var result = _itemHandler.AddItem(null, 20);

            result.Should().NotBeNull();
            result.Exception.Should().Be("product doesn't match.");
            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Result.Should().BeNull();
            result.Success.Should().BeFalse();
        }

        [Fact]
        public void AddItemModelProductNotfoundTest()
        {
            _productHandler.Setup(e => e.GetProduct(It.IsAny<string>())).Returns(new Results<Product>()
            {
                Exception = It.IsAny<string>(),
                HttpStatusCode = It.IsAny<HttpStatusCode>(),
                Success = false,
                Result = null
            }).Verifiable();
            var result = _itemHandler.AddItem("cola", 20);

            _productHandler.Verify(e => e.GetProduct(It.IsAny<string>()), Times.Once);
            result.Should().NotBeNull();
            result.Exception.Should().Be("Product Not found.");
            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
            result.Result.Should().BeNull();
            result.Success.Should().BeFalse();
        }

        [Fact]
        public void AddItemModelNotEnoughstockanleTest()
        {
            _productHandler.Setup(e => e.GetProduct(It.IsAny<string>())).Returns(new Results<Product>()
            {
                Exception = It.IsAny<string>(),
                HttpStatusCode = It.IsAny<HttpStatusCode>(),
                Success = true,
                Result = new Product
                {
                    Id = 6,
                    ProductName = "lease",
                    Price = (decimal).05,
                    StockLevel = 5,
                    BarCode = It.IsAny<string>(),
                    BrandId = It.IsAny<int>(),
                    CategoriesId = It.IsAny<int>(),
                    MinimumWholesaleOrder = It.IsAny<double>(),
                    OrderLevel = It.IsAny<double>(),
                    WholesalePrice = It.IsAny<double>()
                }
            }).Verifiable();

            var result = _itemHandler.AddItem("cola", 20);

            _productHandler.Verify(e => e.GetProduct(It.IsAny<string>()), Times.Once);
            result.Should().NotBeNull();
            result.Exception.Should().Be("we dont have enough Product");
            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().BeNull();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public void AddItemModelWithSuccessTest()
        {
            _productHandler.Setup(e => e.GetProduct(It.IsAny<string>())).Returns(new Results<Product>()
            {
                Exception = It.IsAny<string>(),
                HttpStatusCode = It.IsAny<HttpStatusCode>(),
                Success = true,
                Result = new Product
                {
                    Id = 6,
                    ProductName = "lease",
                    Price = (decimal).05,
                    StockLevel = 50,
                    BarCode = It.IsAny<string>(),
                    BrandId = It.IsAny<int>(),
                    CategoriesId = It.IsAny<int>(),
                    MinimumWholesaleOrder = It.IsAny<double>(),
                    OrderLevel = It.IsAny<double>(),
                    WholesalePrice = It.IsAny<double>(),
                    Brands = new Brand()
                    {
                        BrandName = "lease",
                        Id = It.IsAny<int>()
                    }
                }
            }).Verifiable();

            var result = _itemHandler.AddItem("cola", 20);

            _productHandler.Verify(e => e.GetProduct(It.IsAny<string>()), Times.Once);
            result.Should().NotBeNull();
            result.Exception.Should().BeNullOrWhiteSpace();
            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().NotBeNull();
            result.Success.Should().BeTrue();
        }
    }
}
