//using DataAccess.Entities;
//using FluentAssertions;
//using Moq;
//using Shop.Handlers.Interfaces;
//using Shop.Managers;
//using Shop.Models;
//using System.Collections.Generic;
//using System.Net;
//using Xunit;

//namespace Shop.Tests.ManagersTests
//{
//    public class ProductManagerTest
//    {
//        private readonly Mock<ICategoryHandler> _categoryHandler;
//        private readonly Mock<IProductHandler> _productHandler;
//        private readonly ProductManager _productManager;
//        public ProductManagerTest()
//        {
//            _categoryHandler = new Mock<ICategoryHandler>();
//            _productHandler = new Mock<IProductHandler>();
//            _productManager = new ProductManager(_categoryHandler.Object, _productHandler.Object);
//        }
//        [Fact]
//        public void GetFilteredModelTest()
//        {
//            _categoryHandler
//                .Setup(e => e.Categories())
//                .Returns(new List<Category>() { new Category { Name = "akon" } });
//            _productHandler
//                .Setup(e => e.Products(It.IsAny<int>(), It.IsAny<int>()))
//                .Returns(new List<Product>() { new Product() { ProductName = "coke" } });
//            _productHandler
//                .Setup(e => e.TotalCount(It.IsAny<int>()))
//                .Returns(1);

//            var result = _productManager.GetFilteredModel(1, 1);

//            result.Should().NotBeNull();
//            _categoryHandler
//                .Verify(e => e.Categories(), Times.AtLeastOnce);
//            _productHandler
//                .Verify(e => e.Products(It.IsAny<int>(), It.IsAny<int>()), Times.AtLeastOnce);
//            _productHandler
//                .Verify(e => e.TotalCount(It.IsAny<int>()), Times.AtLeastOnce);
//        }

//        [Fact]
//        public void UpdateStockLevelTest()
//        {
//            var saleproduct = new SaleProduct()
//            {
//                BarCode = It.IsAny<string>(),
//                Brand = It.IsAny<string>(),
//                BrandId = It.IsAny<int>(),
//                CategoriesId = It.IsAny<int>(),
//                Category = It.IsAny<string>(),
//                OrderLevel = It.IsAny<double>(),
//                Price = It.IsAny<decimal>(),
//                ProductId = 1,
//                ProductName = "cola",
//                SaleQuantity = 20
//            };
//            _productHandler
//                .Setup(e => e.RemoveProduct(It.IsAny<SaleProduct>()))
//                .Returns(new Results<SaleProduct>() { Success = true, Exception = null, HttpStatusCode = HttpStatusCode.OK, Result = saleproduct });

//            _productManager.UpdateStockLevel(saleproduct);

//            _productHandler
//                .Verify(e => e.RemoveProduct(It.IsAny<SaleProduct>()), Times.AtLeastOnce);
//        }


//        [Fact]
//        public void GetModel()
//        {
//            _categoryHandler
//                .Setup(e => e.Categories())
//                .Returns(new List<Category>() { new Category { Name = "akon" } });
//            _productHandler
//                .Setup(e => e.Products(It.IsAny<int>()))
//                .Returns(new List<Product>() { new Product() { ProductName = "coke" } });
//            _productHandler
//                .Setup(e => e.TotalCount())
//                .Returns(1);

//            var result = _productManager.GetModel(1);

//            result.Should().NotBeNull();
//            _categoryHandler
//                .Verify(e => e.Categories(), Times.AtLeastOnce);
//            _productHandler
//                .Verify(e => e.Products(It.IsAny<int>()), Times.AtLeastOnce);
//            _productHandler
//                .Verify(e => e.TotalCount(), Times.AtLeastOnce);
//        }
//    }
//}
