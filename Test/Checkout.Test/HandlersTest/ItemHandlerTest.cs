using Checkout.Handlers;
using FluentAssertions;
using Moq;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Checkout.Test.HandlersTest
{
    public class ItemHandlerTest
    {
        private readonly ItemHandler _itemHandler;

        public ItemHandlerTest()
        {
            _itemHandler = new ItemHandler();
        }

        [Fact]
        public void AddItemTest()
        {
            _itemHandler.List = new List<ItemModel>();
            var input = new Results<ItemModel>()
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

            _itemHandler.AddItem(input);

            _itemHandler.List.Count.Should().Be(1);
            _itemHandler.List[0].Should().BeEquivalentTo(new ItemModel()
            {
                Brand = It.IsAny<string>(),
                Id = It.IsAny<int>(),
                Name = It.IsAny<string>(),
                Quantity = It.IsAny<int>(),
                Price = It.IsAny<decimal>(),
                TotalPrice = It.IsAny<double>()
            });
        }

        [Fact]
        public void RemoveItemTest()
        {
            _itemHandler.List = new List<ItemModel>()
            {
                new ItemModel()
                {
                        Brand = "Cola",
                        Id = 1,
                        Name = "Coke",
                        Quantity = 2,
                        Price = (decimal).75,
                        TotalPrice = 1.50
                }
            };

            _itemHandler.RemoveItem(new ItemModel()
            {
                Brand = "Cola",
                Id = 1,
                Name = "Coke",
                Quantity = 2,
                Price = (decimal).75,
                TotalPrice = 1.50
            });

            _itemHandler.List.Count.Should().Be(0);
        }
        [Fact]
        public void GetItemByName()
        {
            _itemHandler.List = new List<ItemModel>()
            {
                new ItemModel()
                {
                        Brand = "Cola",
                        Id = 1,
                        Name = "Coke",
                        Quantity = 2,
                        Price = (decimal).75,
                        TotalPrice = 1.50
                },
                new ItemModel()
                {
                        Brand = "Lease",
                        Id = 2,
                        Name = "Sanks",
                        Quantity = 2,
                        Price = (decimal).5,
                        TotalPrice = 1.0
                }
            };
            var result = _itemHandler.GetItem("Coke");
            _itemHandler.List[0].Should().BeEquivalentTo(result);
        }

        [Fact]
        public void GetItemById()
        {
            _itemHandler.List = new List<ItemModel>()
            {
                new ItemModel()
                {
                        Brand = "Cola",
                        Id = 1,
                        Name = "Coke",
                        Quantity = 2,
                        Price = (decimal).75,
                        TotalPrice = 1.50
                },
                new ItemModel()
                {
                        Brand = "Lease",
                        Id = 2,
                        Name = "Sanks",
                        Quantity = 2,
                        Price = (decimal).5,
                        TotalPrice = 1.0
                }
            };
            var result = _itemHandler.GetItem(1);
            _itemHandler.List[0].Should().BeEquivalentTo(result);
        }

        [Fact]
        public void Total()
        {
            _itemHandler.List = new List<ItemModel>()
            {
                new ItemModel()
                {
                        Brand = "Cola",
                        Id = 1,
                        Name = "Coke",
                        Quantity = 2,
                        Price = (decimal).75,
                        TotalPrice = 1.50
                },
                new ItemModel()
                {
                        Brand = "Lease",
                        Id = 2,
                        Name = "Sanks",
                        Quantity = 2,
                        Price = (decimal).5,
                        TotalPrice = 1.0
                }
            };
            var result = Convert.ToDouble(_itemHandler.Total());
            2.50.Should().Be(result);
        }
    }
}
