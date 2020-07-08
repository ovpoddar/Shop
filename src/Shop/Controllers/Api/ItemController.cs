using Microsoft.AspNetCore.Mvc;
using Shop.Handlers.Interfaces;
using Shop.Utilities;
using System;

namespace Shop.Controllers.Api
{
    [AuthorizeToken]
    [Route("api/")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemHandler _handler;

        public ItemController(IItemHandler handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(_handler));
        }

        [HttpGet("AddProduct")]
        public IActionResult AddProduct(string productName, int Quantity)
        {
            var result = _handler.AddItem(productName, Quantity);
            if (result.Success == false)
                return BadRequest(result);
            return new OkObjectResult(result);
        }
    }
}
