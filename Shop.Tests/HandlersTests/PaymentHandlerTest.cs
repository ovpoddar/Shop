using Checkout.Models;
using DataAccess.Entities;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using Shop.Handlers;
using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class PaymentHandlerTest
    {
        private readonly Mock<IRequestManger> _mock;
        private readonly Mock<IProductHandler> _mock1;
        private readonly PaymentHandler _productHandler;
        public PaymentHandlerTest()
        {
            _mock = new Mock<IRequestManger>();
            _mock1 = new Mock<IProductHandler>();
            _productHandler = new PaymentHandler(_mock.Object, _mock1.Object);
        }

        [Fact]
        public void GetProducts_test()
        {
            _mock1
                .Setup(e => e.GetProduct(It.IsAny<string>()))
                .Returns(Getsome());

            var input = new List<ItemModel>()
            {
                new ItemModel()
                {
                    Id = 1,
                    Brand = It.IsAny<string>(),
                    Name ="cola",
                    Price = (decimal).75,
                    Quantity =20,
                    TotalPrice = 100
                },
            };

            var result = _productHandler.GetProducts(input);

            input.Should().NotBeNull();
        }

        [Fact]
        public async Task PurchaseCallTrueTest()
        {
            var obj = new Results<SaleProduct>()
            {
                Result = new SaleProduct()
                {
                    BarCode = "saniuashsadb",
                    Brand = "astra",
                    BrandId = 1,
                    CategoriesId = 1,
                    StockLevel = 80,
                    ProductName = "mordem",
                    ProductId = 2,
                    Price = 20,
                    OrderLevel = 20,
                    SaleQuantity = 60,
                    Category = "electronics",
                },
                Success = true,
                Exception = null,
                HttpStatusCode = HttpStatusCode.OK
            };
            var successimstring = JsonConvert.SerializeObject(obj);
            _mock.Setup(e => e.PatchRequest("http://localhost:59616/api/Products/StockLevel", It.IsAny<SaleProduct>()))
                .Returns(Task.FromResult(successimstring));

            var result = await _productHandler.PurchaseCall(saleProducts());

            _mock.Verify(e => e.PatchRequest(It.IsAny<string>(), It.IsAny<SaleProduct>()), Times.Once);

            result.Should().Be(true);
        }

        [Fact]
        public async Task PurchaseCallFalseTest()
        {
            var obj = new Results<SaleProduct>()
            {
                Result = new SaleProduct()
                {
                    BarCode = "saniuashsadb",
                    Brand = "astra",
                    BrandId = 1,
                    CategoriesId = 1,
                    StockLevel = 80,
                    ProductName = "mordem",
                    ProductId = 2,
                    Price = 20,
                    OrderLevel = 20,
                    SaleQuantity = 60,
                    Category = "electronics",
                },
                Success = false,
                Exception = null,
                HttpStatusCode = HttpStatusCode.BadRequest
            };
            var successimstring = JsonConvert.SerializeObject(obj);
            _mock.Setup(e => e.PatchRequest("http://localhost:59616/api/Products/StockLevel", It.IsAny<SaleProduct>()))
                .Returns(Task.FromResult(successimstring));

            var result = await _productHandler.PurchaseCall(saleProducts());

            _mock.Verify(e => e.PatchRequest(It.IsAny<string>(), It.IsAny<SaleProduct>()), Times.Once);

            result.Should().Be(false);
        }

        private List<SaleProduct> saleProducts() =>
            new List<SaleProduct>() {
                new SaleProduct
                {
                    BarCode = "saniuashsadb",
                    Brand = "astra",
                    BrandId = 1,
                    CategoriesId = 1,
                    StockLevel = 80,
                    ProductName = "mordem",
                    ProductId = 2,
                    Price = 20,
                    OrderLevel = 20,
                    SaleQuantity = 60,
                    Category = "electronics",
                }
            };

        private Product Getsome() =>
            new Product
            {
                Id = 1,
                ProductName = "cola",
                Price = (decimal).75,
                StockLevel = 150,
                BarCode = "sachbasdhb654612",
                BrandId = 2,
                CategoriesId = 1,
                MinimumWholesaleOrder = 20,
                OrderLevel = 80,
                WholesalePrice = 20,
                Brands = new Brand()
                {
                    BrandName = "xoxo",
                    Id = It.IsAny<int>(),
                },
                Categories = new Category()
                {
                    Name = "yooy",
                    Id = It.IsAny<int>(),
                }
            };
    }
}
