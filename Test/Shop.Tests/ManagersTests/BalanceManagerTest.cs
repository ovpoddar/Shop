using DataAccess.Entities;
using Moq;
using Shop.Handlers.Interfaces;
using Shop.Managers;
using Shop.Models;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Shop.Tests.ManagersTests
{
    public class BalanceManagerTest
    {
        private readonly Mock<IBalanceHandler> _balanceHandler;
        private readonly Mock<IProductHandler> _productHandler;
        private readonly BalanceManager _balanceManager;
        public BalanceManagerTest()
        {
            _balanceHandler = new Mock<IBalanceHandler>();
            _productHandler = new Mock<IProductHandler>();
            _balanceManager = new BalanceManager(_balanceHandler.Object, _productHandler.Object);
        }
        [Fact]
        public void Purchase()
        {
            var product = new Results<Product>()
            {
                Exception = null,
                HttpStatusCode = HttpStatusCode.OK,
                Result = new Product()
                {
                    Id = 1,
                    Price = 2
                },
                Success = true
            };
            var balance = new Balance()
            {
                Ammount = 20
            };

            _productHandler
                .Setup(e => e.GetProduct(It.IsAny<string>()))
                .Returns(product);

            _balanceHandler
                .Setup(e => e.AddBalance(It.IsAny<Balance>()));

            _balanceHandler
                .Setup(e => e.GetLastBalance())
                .Returns(balance);

            _balanceManager.Purchase(itemModels(), 0, It.IsAny<string>());

            _balanceHandler
                .Verify(e => e.AddBalance(It.IsAny<Balance>()), Times.AtLeastOnce);

            _productHandler
                .Verify(e => e.GetProduct(It.IsAny<string>()), Times.AtLeastOnce);
        }

        private List<ItemModel> itemModels() =>
            new List<ItemModel>()
            {
                new ItemModel()
                {
                    Brand = "coca cola",
                    Name ="cola",
                    Quantity =  40,
                    Id = 1,
                    Price = 20,
                    TotalPrice = 800
                }
            };
    }
}
