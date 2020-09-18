using Shop.Handlers.Interfaces;
using Shop.Models;
using System;
using System.Net;

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
            var product = _handler.GetProduct(productName);
            if (product.Success == false)
                return new Results<ItemModel>()
                {
                    Exception = "Product Not found.",
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Result = null,
                    Success = false
                };
            if (product.Result.StockLevel < Quantity)
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
                    Name = product.Result.ProductName,
                    Price = product.Result.Price,
                    Brand = product.Result.Brands.BrandName,
                    Id = product.Result.Id,
                    Quantity = Quantity,
                    TotalPrice = (double)product.Result.Price * Quantity
                },
                Success = true
            };
        }

    }
}
