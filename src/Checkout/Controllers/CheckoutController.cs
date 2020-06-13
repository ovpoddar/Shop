using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shop.Managers.Interfaces;
using System;

namespace Checkout.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public CheckoutController(IProductManager productManager)
        {
            _productManager = productManager ?? throw new ArgumentNullException(nameof(_productManager));
        }

        //[HttpGet("AddProduct")]
        //[EnableCors("All")]
        //public IActionResult AddItem(string productName, int Quentati)
        //{
        //    var result = _handler.AddModel(productName, Quentati);
        //    if (!result.Success) return NotFound(result);
        //    return new OkObjectResult(result);
        //}
    }
}