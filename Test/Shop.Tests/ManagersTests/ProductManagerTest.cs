using DataAccess.Entities;
using FluentAssertions;
using Moq;
using Shop.Handlers.Interfaces;
using Shop.Managers;
using Shop.Managers.Interfaces;
using Shop.Models;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Shop.Tests.ManagersTests
{
    public class ProductManagerTest
    {
        private readonly Mock<ICategoryHandler> _categoryHandler;
        private readonly Mock<IProductHandler> _productHandler;
        private readonly Mock<IBalanceManager> _balanceManager;
        private readonly Mock<IPaymentManager> _paymentManager;
        private readonly ProductManager _productManager;
        public ProductManagerTest()
        {
            _categoryHandler = new Mock<ICategoryHandler>();
            _productHandler = new Mock<IProductHandler>();
            _balanceManager = new Mock<IBalanceManager>();
            _paymentManager = new Mock<IPaymentManager>();
            _productManager = new ProductManager(_categoryHandler.Object, _productHandler.Object, _balanceManager.Object, _paymentManager.Object);
        }
        [Fact]
        public void GetFilteredModelTest()
        {
            _categoryHandler
                .Setup(e => e.Categories())
                .Returns(new List<Category>() { new Category { Name = "akon" } });
            _productHandler
                .Setup(e => e.Products(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Product>() { new Product() { ProductName = "coke" } });
            _productHandler
                .Setup(e => e.TotalCount(It.IsAny<int>()))
                .Returns(1);

            var result = _productManager.GetFilteredModel(1, 1);

            result.Should().NotBeNull();
            _categoryHandler
                .Verify(e => e.Categories(), Times.AtLeastOnce);
            _productHandler
                .Verify(e => e.Products(It.IsAny<int>(), It.IsAny<int>()), Times.AtLeastOnce);
            _productHandler
                .Verify(e => e.TotalCount(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [Fact]
        public void SalesProductWithtrue()
        {
            var model = new PurchaseModel()
            {
                Items = new List<ItemModel>()
                {
                    new ItemModel { Brand = "brand", Id = 1, Name = "name", Price = 20, Quantity = 20, TotalPrice = 400 },
                    new ItemModel { Brand = "brand", Id = 1, Name = "name", Price = 20, Quantity = 20, TotalPrice = 400 }
                },
                PaymentType = 1
            };
            _paymentManager.Setup(e => e.Purchase(It.IsAny<PurchaseModel>())).Returns(new OverallResult<List<Results<ItemModel>>>()
            {
                Objects = new List<Results<ItemModel>>()
                {
                    new Results<ItemModel>()
                    {
                        Success = true,
                        Exception = null,
                        HttpStatusCode = HttpStatusCode.OK,
                        Result = model.Items[0]
                    },
                    new Results<ItemModel>()
                    {
                        Success = true,
                        Exception = null,
                        HttpStatusCode = HttpStatusCode.OK,
                        Result = model.Items[1]
                    }
                },
                Success = true
            });
            _balanceManager.Setup(e => e.Purchase(It.IsAny<List<ItemModel>>(), It.IsAny<uint>(), It.IsAny<string>())).Verifiable();

            var result = _productManager.SalesProduct(model, It.IsAny<string>());

            result.Success.Should().BeTrue();
            result.Objects.Should().NotBeEmpty();
            _balanceManager.Verify(e => e.Purchase(It.IsAny<List<ItemModel>>(), It.IsAny<uint>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void SalesProductWithFalse()
        {
            var model = new PurchaseModel()
            {
                Items = new List<ItemModel>()
                {
                    new ItemModel { Brand = "brand", Id = 1, Name = "name", Price = 20, Quantity = 20, TotalPrice = 400 },
                    new ItemModel { Brand = "brand", Id = 1, Name = "colo", Price = 20, Quantity = 20, TotalPrice = 400 }
                },
                PaymentType = 1
            };
            _paymentManager.Setup(e => e.Purchase(It.IsAny<PurchaseModel>())).Returns(new OverallResult<List<Results<ItemModel>>>()
            {
                Objects = new List<Results<ItemModel>>()
                {
                    new Results<ItemModel>()
                    {
                        Success = true,
                        Exception = null,
                        HttpStatusCode = HttpStatusCode.OK,
                        Result = model.Items[0]
                    },
                    new Results<ItemModel>()
                    {
                        Success = false,
                        Exception = "not found product",
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Result = model.Items[1]
                    }
                },
                Success = false
            });
            _balanceManager.Setup(e => e.Purchase(It.IsAny<List<ItemModel>>(), It.IsAny<uint>(), It.IsAny<string>())).Verifiable();

            var result = _productManager.SalesProduct(model, It.IsAny<string>());

            result.Success.Should().BeFalse();
            result.Objects.Should().NotBeEmpty();
            _balanceManager.Verify(e => e.Purchase(It.IsAny<List<ItemModel>>(), It.IsAny<uint>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void GetModel()
        {
            _categoryHandler
                .Setup(e => e.Categories())
                .Returns(new List<Category>() { new Category { Name = "akon" } });
            _productHandler
                .Setup(e => e.Products(It.IsAny<int>()))
                .Returns(new List<Product>() { new Product() { ProductName = "coke" } });
            _productHandler
                .Setup(e => e.TotalCount())
                .Returns(1);

            var result = _productManager.GetModel(1);

            result.Should().NotBeNull();
            _categoryHandler
                .Verify(e => e.Categories(), Times.AtLeastOnce);
            _productHandler
                .Verify(e => e.Products(It.IsAny<int>()), Times.AtLeastOnce);
            _productHandler
                .Verify(e => e.TotalCount(), Times.AtLeastOnce);
        }
    }
}
