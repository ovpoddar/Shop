using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        [HttpGet]
        [EnableCors("All")]
        public IActionResult AddItem(string productName, int Quentati)
        {

            return null;
        }
    }
}