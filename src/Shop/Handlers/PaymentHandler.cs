using Checkout.Models;
using Newtonsoft.Json;
using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public class PaymentHandler : IPaymentHandler
    {
        private readonly IRequestManger _request;
        private readonly IProductHandler _product;

        public PaymentHandler(IRequestManger request, IProductHandler product)
        {
            _request = request ?? throw new ArgumentNullException(nameof(_request));
            _product = product ?? throw new ArgumentNullException(nameof(_product));
        }

        public List<SaleProduct> GetProducts(List<ItemModel> items)
        {
            var products = new List<SaleProduct>();
            foreach (var item in items)
            {
                var check = _product.GetProduct(item.Name);
                if (check.Price == item.Price)
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

        public async Task<bool> PurchaseCall(List<SaleProduct> products)
        {
            foreach (var product in products)
            {
                if (!JsonConvert.DeserializeObject<Results<SaleProduct>>(await _request.PatchRequest("http://localhost:59616/api/Products/StockLevel", product)).Success)
                    return false;
            }
            return true;
        }

        public Task<bool> SalesCall(List<SaleProduct> products)
        {
            throw new NotImplementedException();
        }
    }
}
