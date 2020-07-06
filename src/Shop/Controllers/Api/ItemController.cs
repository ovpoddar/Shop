using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using Shop.Handlers.Interfaces;
using Shop.Models;

namespace Shop.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public IActionResult AddProduct(string productName,int Quantity)
        {
            var result = _handler.AddItem(productName, Quantity);
            if (result.Success == false)
                return BadRequest(result);
            return new OkObjectResult(result);
        }
    }
}
