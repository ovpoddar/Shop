using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;

namespace Shop.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentManager _manager;

        public PaymentController(IPaymentManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(_manager));
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (Request.Cookies.Keys.Count == 1)
                return RedirectToAction("Index", "Checkout");
            var items = new List<ItemModel>();
            decimal total = 0;
            for (var i = 0; i < Request.Cookies.Keys.Count; i++)
            {
                try
                {
                    var cookie = Get(i.ToString()).Split("=");
                    items.Add(_manager.CreateModel(cookie[0], cookie[1], cookie[2], cookie[3], cookie[4], cookie[5]));
                    total += decimal.Parse(cookie[5]);
                }
                catch
                {
                    continue;
                }
            }
            return View(_manager.GetModel(items, total));
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
                Delete(cookie);
            }
            return RedirectToAction("Index", "Product");
        }

        private string Get(string cName) =>
            Request.Cookies[cName];
        private void Delete(string cName) =>
            Response.Cookies.Delete(cName);
    }
}