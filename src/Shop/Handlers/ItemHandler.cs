using Shop.Handlers.Interfaces;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public class ItemHandler : IItemHandler
    {
        private readonly IProductHandler _handler;

        public ItemHandler(IProductHandler handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(_handler));
        }

        public Results<ItemModel> AddItem(string productName, int Quantity)
        {
            if (string.IsNullOrWhiteSpace(productName))
                return new Results<ItemModel>()
                {
                    Exception = "product doesn't match.",
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Result = null,
                    Success = false
                };
            var product = _handler.GetProduct(productName).Result;
            if (product == null)
                return new Results<ItemModel>()
                {
                    Exception = "Product Not found.",
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Result = null,
                    Success = false
                };
            if(product.StockLevel < Quantity)
                return new Results<ItemModel>()
                {
                    Exception = "we dont have enough Product",
                    HttpStatusCode = HttpStatusCode.OK,
                    Result = null,
                    Success = true
                };
            return new Results<ItemModel>()
            {
                Exception = null,
                HttpStatusCode = HttpStatusCode.OK,
                Result = new ItemModel() 
                { 
                    Name = product.ProductName,
                    Price = product.Price,
                    Brand = product.Brands.BrandName,
                    Id = product.Id,
                    Quantity = Quantity,
                    TotalPrice = (double)product.Price * Quantity
                },
                Success = true
            };
        }

    }
}
