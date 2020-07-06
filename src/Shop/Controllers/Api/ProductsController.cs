using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.ActionFilters;
using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
using System.Linq;
using System.Net;

namespace Shop.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;
        private readonly IProductHandler _productHandler;

        public ProductsController(IProductManager productManager, IProductHandler productHandler)
        {
            _productHandler = productHandler ?? throw new System.ArgumentNullException(nameof(_productHandler));
            _productManager = productManager ?? throw new System.ArgumentNullException(nameof(productManager));
        }

        [HttpGet("GetProduct")]
        public IActionResult Getproduct(string Name)
        {
            var result = _productHandler.GetProduct(Name);
            if (result.HttpStatusCode == HttpStatusCode.InternalServerError) return new NotFoundResult();
            return new OkObjectResult(result);

        }

        [HttpPatch("Purchase")]
        [EnableCors("All")]
        [ServiceFilter(typeof(ProductActionFilter))]
        public IActionResult PurchaseProduct([FromBody] PurchaseModel model)
        {
            return new OkObjectResult(_productManager.SalesProduct(model, HttpContext.User.Claims.FirstOrDefault(e => e.Type == "iat").Value));
        }
    }
}
