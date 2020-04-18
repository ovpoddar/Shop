using Microsoft.AspNetCore.Mvc;
using Shop.Managers;
using System.Net;
using Shop.ActionFilters;
using Shop.Entities;
using Shop.Models;

namespace Shop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController
    {
        private readonly IProductManager _productManager;

        public ProductsController(IProductManager productManager) => 
            _productManager = productManager;

        [HttpGet("{productId}")]
        public IActionResult GetProduct(int productId)
        {
            var results = _productManager.GetProductById(productId);
            if(results.HttpStatusCode == HttpStatusCode.InternalServerError) return new NotFoundResult();
            
            return new OkObjectResult(results.Result);
        }

        [HttpPatch("StockLevel")]
        [ServiceFilter(typeof(ProductActionFilter))]
        public IActionResult UpdateProductStockLevel([FromBody] SaleProduct saleProduct)
        {
            var results = _productManager.UpdateStockLevel(saleProduct);
            if (results.HttpStatusCode == HttpStatusCode.InternalServerError) return new NotFoundResult();
            return new OkObjectResult(results.Result);
        }

        [HttpPost("Brand")]
        [ServiceFilter(typeof(ProductActionFilter))]
        public IActionResult CreateBrand(Brand brand)
        {
            var results = _productManager.AddBrand(brand);
            if (results.HttpStatusCode == HttpStatusCode.InternalServerError) return new NotFoundResult();

            return new OkObjectResult(results.Result);
        }
    }
}
