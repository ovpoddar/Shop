﻿using Microsoft.AspNetCore.Mvc;
using Shop.ActionFilters;
using Shop.Managers;
using Shop.Models;
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
    }
}
