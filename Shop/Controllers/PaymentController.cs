using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using Shop.Managers;
using Shop.ViewModels;
using System;
using System.Collections.Generic;

namespace Shop.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentManager _payment;
        private readonly IBalanceManager _balance;

        public PaymentController(IPaymentManager payment, IBalanceManager balance)
        {
            _payment = payment ?? throw new ArgumentNullException(nameof(_payment));
            _balance = balance ?? throw new ArgumentNullException(nameof(_balance));
        }
        [HttpGet]
        public IActionResult Index()
        {
            var list = new List<ItemViewModel>();
            if (Request.Cookies.Keys.Count == 1)
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
        public IActionResult Index(List<ItemViewModel> models, uint Payment)
        {
            _payment.PurchaseCall(models);
            _balance.Purchase(models, Payment);
            foreach (var cookie in Request.Cookies.Keys)
            {
                if (cookie.Length > 3)
                    continue;
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Index", "Product");
        }
    }
}