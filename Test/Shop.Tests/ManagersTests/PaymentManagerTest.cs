using FluentAssertions;
using Moq;
using Shop.Handlers.Interfaces;
using Shop.Managers;
using Shop.Models;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Shop.Tests.ManagersTests
{
    public class PaymentManagerTest
    {
        private readonly Mock<IProductHandler> _productHandler;
        private readonly PaymentManager _paymentManager;
        public PaymentManagerTest()
        {
            _productHandler = new Mock<IProductHandler>();
            _paymentManager = new PaymentManager(_productHandler.Object);
        }

        [Fact]
        public void PurchaseWithSuccess()
        {
            var model = new PurchaseModel()
            {
                Items = new List<ItemModel>()
                {
                    new ItemModel { Brand = "brand", Id = 1, Name = "name", Price = 20, Quantity = 20, TotalPrice = 400 }
                },
                PaymentType = 1
            };

            _productHandler.Setup(e => e.RemoveProduct(It.IsAny<ItemModel>())).Returns(new Results<ItemModel>()
            {
                Exception = null,
                HttpStatusCode = HttpStatusCode.OK,
                Result = It.IsAny<ItemModel>(),
                Success = true
            }).Verifiable();

            var result = _paymentManager.Purchase(model);

            result.Success.Should().BeTrue();
            _productHandler.Verify(e => e.RemoveProduct(It.IsAny<ItemModel>()), Times.Once);
        }

        [Fact]
        public void PurchaseWithUnsuccess()
        {
            var model = new PurchaseModel()
            {
                Items = new List<ItemModel>()
                {
                    new ItemModel { Brand = "brand", Id = 1, Name = "name", Price = 20, Quantity = 20, TotalPrice = 400 }
                },
                PaymentType = 1
            };

            _productHandler.Setup(e => e.RemoveProduct(It.IsAny<ItemModel>())).Returns(new Results<ItemModel>()
            {
                Exception = null,
                HttpStatusCode = HttpStatusCode.OK,
                Result = It.IsAny<ItemModel>(),
                Success = false
            }).Verifiable();

            var result = _paymentManager.Purchase(model);

            result.Success.Should().BeFalse();
            _productHandler.Verify(e => e.RemoveProduct(It.IsAny<ItemModel>()), Times.Once);
        }

        [Fact]
        public void Purchasemultipalproduct()
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

            _productHandler.Setup(e => e.RemoveProduct(It.IsAny<ItemModel>())).Returns(new Results<ItemModel>()
            {
                Exception = null,
                HttpStatusCode = HttpStatusCode.OK,
                Result = It.IsAny<ItemModel>(),
                Success = false
            }).Verifiable();

            var result = _paymentManager.Purchase(model);

            result.Success.Should().BeFalse();
            _productHandler.Verify(e => e.RemoveProduct(It.IsAny<ItemModel>()), Times.AtLeast(model.Items.Count));
        }
    }
}
