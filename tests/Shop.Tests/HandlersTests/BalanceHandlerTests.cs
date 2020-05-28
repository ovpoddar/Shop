using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Entities;
using Shop.Handlers;
using Shop.Repositories;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class BalanceHandlerTests
    {
        private readonly Mock<IGenericRepository<Balance>> _mock;
        private readonly BalanceHandler _balanceHandler;
        public BalanceHandlerTests()
        {
            _mock = new Mock<IGenericRepository<Balance>>();
            _balanceHandler = new BalanceHandler(_mock.Object);
        }

        [Fact]
        public void GetLastBalance_Balance_one()
        {
            _mock
                .Setup(e => e.GetAll())
                .Returns(GetOneData());

            _balanceHandler.GetLastBalance().Should().BeEquivalentTo(GetOneData().ToList()[^1]);
        }
        [Fact]
        public void GetLastBalance_Balance_Multipal()
        {
            _mock
                .Setup(e => e.GetAll())
                .Returns(GetMoreThanData());

            _balanceHandler.GetLastBalance().Should().BeEquivalentTo(GetMoreThanData().ToList()[^1]);
        }

        [Fact]
        public void AddBalance_void_working()
        {
            _mock.Setup(e => e.Add(It.IsAny<Balance>()));

            _balanceHandler.AddBalance(It.IsAny<Balance>());

            _mock.Verify(e => e.Add(It.IsAny<Balance>()), Times.Once());
            _mock.Verify(e => e.Save(), Times.Once());
        }

        private IQueryable<Balance> GetOneData() =>
            new List<Balance>()
            {
                new Balance()
                {
                    Id = 1,
                    Ammount = 500,
                    Date = new DateTime(2020,05,12,16,17,20,500),
                    Incoming = 500,
                    Outgoing = 0,
                    PaymentTypeId = 1,
                    ProductId = 1,
                    Quantity = 40
                }
            }.AsQueryable();

        private IQueryable<Balance> GetMoreThanData() =>
            new List<Balance>()
            {
                new Balance()
                {
                    Id = 1,
                    Ammount = 500,
                    Date = new DateTime(2020,05,12,16,17,20,500),
                    Incoming = 500,
                    Outgoing = 0,
                    PaymentTypeId = 1,
                    ProductId = 1,
                    Quantity = 40
                },
                new Balance()
                {
                    Id = 2,
                    Ammount = 560,
                    Date = new DateTime(2020,05,12,16,17,20,500),
                    Incoming = 60,
                    Outgoing = 0,
                    PaymentTypeId = 0,
                    ProductId = 1,
                    Quantity = 20
                },
                new Balance()
                {
                    Id = 3,
                    Ammount = 600,
                    Date = new DateTime(2020,05,12,16,17,20,500),
                    Incoming = 40,
                    Outgoing = 0,
                    PaymentTypeId = 0,
                    ProductId = 1,
                    Quantity = 20
                }

            }.AsQueryable();

    }
}
