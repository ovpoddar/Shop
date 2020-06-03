using DataAccess.Entities;
using FluentAssertions;
using Shop.Handlers;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class ItemHandlerTest
    {
        private readonly ItemHandler _itemHandler;
        public ItemHandlerTest()
        {
            _itemHandler = new ItemHandler();
        }

        [Fact]
        public void addItemTest()
        {
            var newData = new Product()
            {
                ProductName = "mint",
                Price = (decimal).1,
                Brands = new Brand()
                {
                    BrandName = "coloromint",
                    Id = 1
                }
            };

            _itemHandler.addItem(newData, 60);

            Assert.NotEqual(1, _itemHandler.List.Count);
            Assert.Equal(2, _itemHandler.List.Count);
            Assert.Equal(_itemHandler.List[^1].Name, newData.ProductName);
            Assert.Equal(1, _itemHandler.List[^1].Id);
            Assert.Equal(_itemHandler.List[^1].Price, newData.Price);
            Assert.Equal(60, _itemHandler.List[^1].Quantity);
        }

        [Fact]
        public void RemoveItemTest()
        {
            var newData = new Product()
            {
                ProductName = "mint",
                Price = (decimal).1,
                Brands = new Brand()
                {
                    BrandName = "coloromint",
                    Id = 1
                }
            };

            _itemHandler.addItem(newData, 60);
            _itemHandler.RemoveItem(newData);

            Assert.Single(_itemHandler.List);
        }


        [Fact]
        public void GetItemTest()
        {
            var newData = new Product()
            {
                ProductName = "mint",
                Price = (decimal).1,
                Brands = new Brand()
                {
                    BrandName = "coloromint",
                    Id = 1
                }
            };
            _itemHandler.addItem(newData, 60);
            var result = _itemHandler.GetItem("mint");

            result.Should().BeEquivalentTo(_itemHandler.List[1]);
        }
    }
}
