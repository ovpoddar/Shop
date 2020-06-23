using Shop.Handlers.Interfaces;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public class PaymentHandler : IPaymentHandler
    {
        private readonly IProductHandler _productHandler;

        public PaymentHandler(IProductHandler productHandler)
        {
            _productHandler = productHandler ?? throw new ArgumentNullException(nameof(_productHandler));
        }

        public List<SaleProduct> GetProducts(List<ItemModel> items)
        {
            var products = new List<SaleProduct>();
            foreach (var item in items)
            {
                var check = _productHandler.GetProduct(item.Name).Result;
                products.Add(new SaleProduct
                {
                    BarCode = check.BarCode,
                    ProductId = check.Id,
                    ProductName = check.ProductName,
                    Price = check.Price,
                    StockLevel = check.StockLevel,
                    OrderLevel = check.OrderLevel,
                    CategoriesId = check.CategoriesId,
                    Category = check.Categories.Name,
                    BrandId = check.BrandId,
                    Brand = check.Brands.BrandName,
                    SaleQuantity = item.Quantity
                });
            }
            return products;
        }


        //public async Task<bool> PurchaseCall(List<SaleProduct> products)
        //{
        //    //foreach (var product in products)
        //    //{
        //    //    if (!JsonConvert.DeserializeObject<Results<SaleProduct>>(await _requestManger.PatchRequest("http://localhost:59616/api/Products/StockLevel", product)).Success)
        //    //        return false;
        //    //}
        //    return true;
        //}
        public Task<bool> SalesCall(List<SaleProduct> products)
        {
            throw new NotImplementedException();
        }
    }
}
