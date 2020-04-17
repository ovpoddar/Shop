using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using System;

namespace Shop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceControlController : ControllerBase
    {
        private readonly IProductHandler _product;

        public PriceControlController(IProductHandler product)
        {
            _product = product ?? throw new ArgumentNullException(nameof(_product));
        }
        //[HttpGet("{id}")]
        ////[Route("api/PriceP")]
        //public decimal PriceP(int id, int Quantity)
        //{
        //    return new OkObjectResult(result.ResponseData);
        //    //return _product.GetProduct(id).Price * Quantity;
        //}

        [HttpPost]
        [Route("api/Buy")]
        public void Buy(int id, uint Qunatity)
        {
            var product = _product.GetProduct(id);
            if (product.ProductName == null)
                return;
            _product.RemoveProduct(product, Qunatity);
        }
    }
}