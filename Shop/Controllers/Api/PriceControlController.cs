using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using System;

namespace Shop.Controllers.Api
{
    [ApiController]
    public class PriceControlController : ControllerBase
    {
        private readonly IProductHandler _product;

        public PriceControlController(IProductHandler product)
        {
            _product = product ?? throw new ArgumentNullException(nameof(_product));
        }
        [HttpGet]
        [Route("api/PriceP")]
        public decimal PriceP(int id, int Quantity)
        {
            return _product.GetProduct(id).Price * Quantity;
        }

        [HttpPost]
        [Route("api/Buy")]
        public bool Buy(int id, int Qunatity)
        {
            var product = _product.GetProduct(id);
            if (product.ProductName == null)
                return false; 
            _product.RemoveProduct(product, Qunatity);
            return true;
        }
    }
}