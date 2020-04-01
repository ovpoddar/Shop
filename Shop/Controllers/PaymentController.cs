using Microsoft.AspNetCore.Mvc;
using Shop.Managers;
using Shop.ViewModels;
using System.Collections.Generic;

namespace Shop.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentManager _payment;

        public PaymentController(IPaymentManager payment)
        {
            _payment = payment ?? throw new System.ArgumentNullException(nameof(_payment));
        }
        [HttpGet]
        public IActionResult Index()
        {
            var list = new List<ItemViewModel>();
            if (Request.Cookies.Keys.Count == 0)
                return RedirectToAction("Index", "Product");
            foreach (var cookie in Request.Cookies.Keys)
            {
                if (cookie.Length > 3)
                    continue;
                var nos = Request.Cookies[cookie].Split("+");
                list.Add(_payment.GetViewModels(nos[0], nos[1]));
            }
            return View(list);
        }
        [HttpPost]
        public IActionResult Index(List<ItemViewModel> models)
        {
            return View();
        }
    }
}