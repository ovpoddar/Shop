using FluentAssertions;
using Moq;
using Shop.Entities;
using Shop.Handlers.Interfaces;
using Shop.Managers;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Shop.Tests.ManagersTests
{
    public class ItemManagerTest
    {
        private readonly Mock<IProductHandler> _productHandler;
        private readonly Mock<IItemHandler<ItemModel>> _itemHandler;
        private readonly ItemManager _itemManager;
        public ItemManagerTest()
        {
            _productHandler = new Mock<IProductHandler>();
            _itemHandler = new Mock<IItemHandler<ItemModel>>();
            _itemManager = new ItemManager(_productHandler.Object, _itemHandler.Object);
        }

        [Fact]
        public void addTest()
        {
            var product = new Product
            {
                Id = 1,
                ProductName = "cola",
                Price = (decimal).75,
                StockLevel = 50,
                BarCode = It.IsAny<string>(),
                BrandId = It.IsAny<int>(),
                CategoriesId = It.IsAny<int>(),
                MinimumWholesaleOrder = It.IsAny<double>(),
                OrderLevel = It.IsAny<double>(),
                WholesalePrice = It.IsAny<double>()
            };

            _productHandler
                .Setup(e => e.GetProduct(It.IsAny<string>()))
                .Returns(product);
            _itemHandler
                .Setup(e => e.List)
                .Returns(new List<ItemModel>() { new ItemModel { Brand = "brand", Id = 1, Name = "name", Price = 20, Quantity = 20, TotalPrice = 400 } });
            _itemHandler
                .Setup(e => e.RemoveItem(It.IsAny<Product>()));
            _itemHandler
                .Setup(e => e.addItem(It.IsAny<Product>(), It.IsAny<int>()));


            _itemManager.add(new ItemViewModel
            {
                Name = "name",
                Quantity = 20,
            });

            _productHandler
               .Verify(e => e.GetProduct(It.IsAny<string>()), Times.Once);
            _itemHandler
                .Verify(e => e.RemoveItem(It.IsAny<Product>()), Times.Once);
            _itemHandler
                .Verify(e => e.addItem(It.IsAny<Product>(), It.IsAny<int>()), Times.Once);
        }



        [Fact]
        public void ModelTest()
        {
            var product = new Product
            {
                Id = 1,
                ProductName = "cola",
                Price = (decimal).75,
                StockLevel = 50,
                BarCode = It.IsAny<string>(),
                BrandId = It.IsAny<int>(),
                CategoriesId = It.IsAny<int>(),
                MinimumWholesaleOrder = It.IsAny<double>(),
                OrderLevel = It.IsAny<double>(),
                WholesalePrice = It.IsAny<double>()
            };

            _productHandler
                .Setup(e => e.GetProduct(It.IsAny<string>()))
                .Returns(product);

            _itemHandler
                .Setup(e => e.GetItem(It.IsAny<string>()))
                .Returns(new ItemModel()
                {
                    Brand = "brand",
                    Id = 1,
                    Name = "cola",
                    Price = 20,
                    Quantity = 20,
                    TotalPrice = 400
                });

            var result = _itemManager.Model("name");

            Assert.Equal(result.Name, product.ProductName);
        }

        [Fact]
        public void GetItemTest()
        {
            _itemHandler
                .Setup(e => e.GetItem(It.IsAny<string>()));

            _itemManager.GetItem("name");

            _itemHandler
                .Verify(e => e.GetItem(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void removeTest()
        {
            _itemHandler
                .Setup(e => e.List)
                .Returns(new List<ItemModel>() { new ItemModel { Brand = "brand", Id = 1, Name = "name", Price = 20, Quantity = 20, TotalPrice = 400 } });

            _itemManager.remove(1);

            _itemHandler
                .Verify(e => e.List, Times.AtLeast(2));
        }
    }
}
