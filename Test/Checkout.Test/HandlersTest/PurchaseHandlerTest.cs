using Checkout.Handlers;
using Checkout.Managers;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using Shop.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Test.HandlersTest
{
    public class PurchaseHandlerTest
    {
        private readonly Mock<IRequestManger> _requestManger;
        private readonly PurchaseHandler _purchaseHandler;

        public PurchaseHandlerTest()
        {
            _requestManger = new Mock<IRequestManger>();
            _purchaseHandler = new PurchaseHandler(_requestManger.Object);
        }

        [Fact]
        public async Task MakePurchaseCallAsyncTestAsync()
        {
            var model = new PurchaseModel()
            {
                Items = new List<ItemModel>()
                {
                    new ItemModel()
                    {
                        Brand = "cola",
                         Id =1,
                         Name = "coke",
                          Price = (decimal)1.5,
                          Quantity = 20,
                          TotalPrice = 30
                    }
                },
                PaymentType = 1
            };
            var returnobject = new OverallResult<List<Results<ItemModel>>>()
            {
                Objects = new List<Results<ItemModel>>()
                {
                    new Results<ItemModel>()
                    {
                        Exception = null,
                        HttpStatusCode = HttpStatusCode.OK,
                        Result = new ItemModel()
                        {
                            Brand = "cola",
                             Id =1,
                             Name = "coke",
                              Price = (decimal)1.5,
                              Quantity = 20,
                              TotalPrice = 30
                        },
                        Success =true
                    },
                    new Results<ItemModel>()
                    {
                        Exception = null,
                        HttpStatusCode = HttpStatusCode.OK,
                        Result = new ItemModel()
                        {
                            Brand = "cola",
                             Id = 2,
                             Name = "coke",
                              Price = (decimal)1.5,
                              Quantity = 20,
                              TotalPrice = 30
                        },
                        Success =true
                    }
                },
                Success = true
            };
            _requestManger.Setup(e => e.PatchRequest(It.IsAny<string>(), model, It.IsAny<string>())).Returns(Task.FromResult(JsonConvert.SerializeObject(returnobject)));

            var result = await _purchaseHandler.MakePurchaseCallAsync(model, "it is the token");

            result.Objects.Count.Should().Be(2);
            result.Should().NotBeNull();
        }
    }
}
