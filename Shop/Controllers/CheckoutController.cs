using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}