using Microsoft.AspNetCore.Mvc;
using Shop.Managers;
using System.Net;

namespace Shop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController
    {
        private readonly IProductManager _productManager;

        public ProductsController(IProductManager productManager) => 
            _productManager = productManager;


        [HttpGet("{id}")]
        public IActionResult GetProduct(int productId)
        {
            var results = _productManager.GetProductById(productId);
            if(results.HttpStatusCode == HttpStatusCode.InternalServerError) return new NotFoundResult();
            
            return new OkObjectResult(results.Result);
        }

        //[HttpPost]
        //[ServiceFilter(typeof(ProductActionFilter))]
        //public IActionResult UpdateProductStockLevel([FromBody]SaleProduct saleProduct)
        //{
        //    var result = _productManager.UpdateStockLevel(saleProduct]);
        //}
    }
}
