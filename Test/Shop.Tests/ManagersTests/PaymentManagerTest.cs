using FluentAssertions;
using Moq;
using Shop.Handlers.Interfaces;
using Shop.Managers;
using Shop.Managers.Interfaces;
using Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests.ManagersTests
{
    public class PaymentManagerTest
    {
        private readonly Mock<IBalanceManager> _balanceManager;
        private readonly Mock<IPaymentHandler> _paymentHandler;
        private readonly PaymentManager _paymentManager;
        public PaymentManagerTest()
        {
            _balanceManager = new Mock<IBalanceManager>();
            _paymentHandler = new Mock<IPaymentHandler>();
            _paymentManager = new PaymentManager(_balanceManager.Object, _paymentHandler.Object);
        }

        [Fact]
        public async Task MakeingPaymentAsyncTestAsyncTrue()
        {
            var list = new List<ItemModel>() { new ItemModel { Brand = "brand", Id = 1, Name = "name", Price = 20, Quantity = 20, TotalPrice = 400 } };

            _paymentHandler
                .Setup(e => e.PurchaseCall(It.IsAny<List<SaleProduct>>()))
                .Returns(Task.FromResult(true));
            _paymentHandler
                .Setup(e => e.GetProducts(It.IsAny<List<ItemModel>>()))
                .Returns(It.IsAny<List<SaleProduct>>());
            _balanceManager
                .Setup(e => e.Purchase(It.IsAny<List<ItemModel>>(), 1));

            var result = await _paymentManager.MakeingPurchaseAsync(list, 1);

            result.Should().BeTrue();
            _paymentHandler
                .Verify(e => e.PurchaseCall(It.IsAny<List<SaleProduct>>()), Times.AtLeastOnce);
            _paymentHandler
                .Verify(e => e.GetProducts(It.IsAny<List<ItemModel>>()), Times.AtLeastOnce);
        }
    }
}
