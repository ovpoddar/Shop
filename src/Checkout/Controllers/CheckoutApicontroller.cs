using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Handlers.Interfaces;

namespace Checkout.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IProductHandler _handler;

        public CheckoutController(IProductHandler handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(_handler));
        }

        [HttpGet("AddProduct")]
        [EnableCors("All")]
        public string Add(string productName, int Quentati)
        {
            var product = _handler.GetProduct(productName);
            return $"name {product.ProductName} quantity {Quentati}";
        }
    }
}
