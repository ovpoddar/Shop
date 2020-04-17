using Microsoft.AspNetCore.Mvc;
using Shop.Managers;
using System.Net;
using Shop.ActionFilters;
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


        [HttpGet("{id}")]
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
            return new OkResult();
        }
        //[HttpPost]
      
        //public IActionResult UpdateProductStockLevel([FromBody]SaleProduct saleProduct)
        //{
        //    var result = _productManager.UpdateStockLevel(saleProduct]);
        //}


        //[HttpPost]
        //[Route("api/Buy")]
        //public void Buy(int id, uint Qunatity)
        //{
        //    var product = _product.GetProduct(id);
        //    if (product.ProductName == null)
        //        return;
        //    _product.RemoveProduct(product, Qunatity);
        //}
    }
}
