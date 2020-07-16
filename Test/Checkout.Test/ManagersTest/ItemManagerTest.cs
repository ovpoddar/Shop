using Checkout.Handlers;
using Checkout.Managers;
using Checkout.ViewModels;
using DataAccess.Entities;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using Shop.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Test.ManagersTest
{
    public class ItemManagerTest
    {
        private readonly Mock<IItemHandler<ItemModel>> _mockHandler;
        private readonly Mock<IRequestManger> _mockRequest;
        private readonly ItemManager _itemManager;
        public ItemManagerTest()
        {
            _mockHandler = new Mock<IItemHandler<ItemModel>>();
            _mockRequest = new Mock<IRequestManger>();
            _itemManager = new ItemManager(_mockHandler.Object, _mockRequest.Object);
        }

        [Fact]
        public async Task AddTestWithSuccessAsync()
        {
            var model = new ItemViewModel()
            {
                Name = "Cola",
                Quantity = 20
            };
            var returnjson = new Results<ItemModel>()
            {
                Exception = null,
                HttpStatusCode = HttpStatusCode.OK,
                Result = new ItemModel()
                {
                    Brand = It.IsAny<string>(),
                    Id = It.IsAny<int>(),
                    Name = It.IsAny<string>(),
                    Quantity = It.IsAny<int>(),
                    Price = It.IsAny<decimal>(),
                    TotalPrice = It.IsAny<double>()
                },
                Success = true
            };
            _mockRequest.Setup(e => e.GetRequest(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(JsonConvert.SerializeObject(returnjson)));
            _mockHandler.Setup(e => e.List).Returns(new List<ItemModel>());
            _mockHandler.Setup(e => e.RemoveItem(It.IsAny<ItemModel>())).Verifiable();
            _mockHandler.Setup(e => e.AddItem(It.IsAny<Results<ItemModel>>())).Verifiable();



            var result = await _itemManager.Add(model, It.IsAny<string>());

            result.Should().BeNull();
            _mockHandler.Verify(e => e.AddItem(It.IsAny<Results<ItemModel>>()), Times.Once);
            _mockHandler.Verify(e => e.RemoveItem(It.IsAny<ItemModel>()), Times.Never);
        }

        [Fact]
        public async Task AddTestWithSuccessWithExistingrecordAsync()
        {
            var model = new ItemViewModel()
            {
                Name = "Cola",
                Quantity = 20
            };
            var returnjson = new Results<ItemModel>()
            {
                Exception = null,
                HttpStatusCode = HttpStatusCode.OK,
                Result = new ItemModel()
                {
                    Brand = It.IsAny<string>(),
                    Id = It.IsAny<int>(),
                    Name = "Cola",
                    Quantity = It.IsAny<int>(),
                    Price = It.IsAny<decimal>(),
                    TotalPrice = It.IsAny<double>()
                },
                Success = true
            };
            _mockRequest.Setup(e => e.GetRequest(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(JsonConvert.SerializeObject(returnjson)));
            _mockHandler.Setup(e => e.List).Returns(new List<ItemModel>()
            {
                new ItemModel()
                {
                    Name = "Cola",
                    Quantity = 20
                }
            });
            _mockHandler.Setup(e => e.RemoveItem(It.IsAny<ItemModel>())).Verifiable();
            _mockHandler.Setup(e => e.AddItem(It.IsAny<Results<ItemModel>>())).Verifiable();



            var result = await _itemManager.Add(model, It.IsAny<string>());

            result.Should().BeNull();
            _mockHandler.Verify(e => e.AddItem(It.IsAny<Results<ItemModel>>()), Times.Once);
            _mockHandler.Verify(e => e.RemoveItem(It.IsAny<ItemModel>()), Times.Once);
        }

        [Fact]
        public async Task AddTestWithSuccessFalseAsync()
        {
            var model = new ItemViewModel()
            {
                Name = "Cola",
                Quantity = 20
            };
            var returnjson = new Results<ItemModel>()
            {
                Exception = null,
                HttpStatusCode = HttpStatusCode.OK,
                Result = new ItemModel()
                {
                    Brand = It.IsAny<string>(),
                    Id = It.IsAny<int>(),
                    Name = It.IsAny<string>(),
                    Quantity = It.IsAny<int>(),
                    Price = It.IsAny<decimal>(),
                    TotalPrice = It.IsAny<double>()
                },
                Success = false
            };
            _mockRequest.Setup(e => e.GetRequest(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(JsonConvert.SerializeObject(returnjson)));
            _mockHandler.Setup(e => e.List).Returns(new List<ItemModel>());
            _mockHandler.Setup(e => e.RemoveItem(It.IsAny<ItemModel>())).Verifiable();
            _mockHandler.Setup(e => e.AddItem(It.IsAny<Results<ItemModel>>())).Verifiable();



            var result = await _itemManager.Add(model, It.IsAny<string>());

            result.Should().Be("Request Fail");
            _mockHandler.Verify(e => e.AddItem(It.IsAny<Results<ItemModel>>()), Times.Never);
            _mockHandler.Verify(e => e.RemoveItem(It.IsAny<ItemModel>()), Times.Never);
        }

        [Fact]
        public async Task AddTestWithSuccessButNoResultAsync()
        {
            var model = new ItemViewModel()
            {
                Name = "Cola",
                Quantity = 20
            };
            var returnjson = new Results<ItemModel>()
            {
                Exception = "No ProductFound",
                HttpStatusCode = HttpStatusCode.OK,
                Result = null,
                Success = true
            };
            _mockRequest.Setup(e => e.GetRequest(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(JsonConvert.SerializeObject(returnjson)));
            _mockHandler.Setup(e => e.List).Returns(new List<ItemModel>());
            _mockHandler.Setup(e => e.RemoveItem(It.IsAny<ItemModel>())).Verifiable();
            _mockHandler.Setup(e => e.AddItem(It.IsAny<Results<ItemModel>>())).Verifiable();



            var result = await _itemManager.Add(model, It.IsAny<string>());

            result.Should().Be(returnjson.Exception);
            _mockHandler.Verify(e => e.AddItem(It.IsAny<Results<ItemModel>>()), Times.Never);
            _mockHandler.Verify(e => e.RemoveItem(It.IsAny<ItemModel>()), Times.Never);
        }

        [Fact]
        public void GetItemTest()
        {
            _mockHandler.Setup(e => e.GetItem(It.IsAny<string>())).Returns(new ItemModel()
            {
                Brand = It.IsAny<string>(),
                Id = It.IsAny<int>(),
                Name = It.IsAny<string>(),
                Quantity = It.IsAny<int>(),
                Price = It.IsAny<decimal>(),
                TotalPrice = It.IsAny<double>()
            });

            var result = _itemManager.GetItem(It.IsAny<string>());

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task ModelTestAsync()
        {

            var returnjson = new Results<Product>()
            {
                Exception = null,
                HttpStatusCode = HttpStatusCode.OK,
                Result = new Product()
                {
                    Id = 002,
                    ProductName = "hammer",
                    Price = (decimal)3.99,
                    StockLevel = 50,
                    BarCode = It.IsAny<string>(),
                    BrandId = It.IsAny<int>(),
                    CategoriesId = It.IsAny<int>(),
                    MinimumWholesaleOrder = It.IsAny<double>(),
                    OrderLevel = It.IsAny<double>(),
                    WholesalePrice = It.IsAny<double>()
                },
                Success = true
            };
            _mockRequest.Setup(e => e.GetRequest(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(JsonConvert.SerializeObject(returnjson)));
            _mockHandler.Setup(e => e.GetItem(It.IsAny<string>())).Returns(new ItemModel()
            {
                Quantity = 20
            });

            var result = await _itemManager.Model(It.IsAny<string>(), It.IsAny<string>());

            result.Should().NotBeNull();
        }

        [Fact]
        public void removeTest()
        {
            _mockHandler.Setup(e => e.RemoveItem(It.IsAny<ItemModel>())).Verifiable();
            _mockHandler.Setup(e => e.GetItem(It.IsAny<int>())).Returns(new ItemModel()
            {
                Brand = It.IsAny<string>(),
                Id = It.IsAny<int>(),
                Name = It.IsAny<string>(),
                Quantity = It.IsAny<int>(),
                Price = It.IsAny<decimal>(),
                TotalPrice = It.IsAny<double>()
            });

            _itemManager.remove(It.IsAny<int>());

            _mockHandler.Verify(e => e.RemoveItem(It.IsAny<ItemModel>()), Times.Once);
        }
    }
}
