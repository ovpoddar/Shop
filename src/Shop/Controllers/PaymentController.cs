using Microsoft.AspNetCore.Mvc;
using Shop.Managers;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var model = new PaymentViewModel()
            {
                Items = new List<ItemModel>(),
                Total = 0
            };
            for (var i = 0; i < Request.Cookies.Keys.Count; i++)
            {
                try
                {
                    var cookie = Get(i.ToString()).Split("=");
                    model.Items.Add(_manager.CreateModel(cookie[0], cookie[1], cookie[2], cookie[3], cookie[4], cookie[5]));
                    model.Total += decimal.Parse(cookie[5]);
                }
                catch
                {
                    continue;
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(List<ItemModel> Items, uint Payment)
        {
            if (!await _manager.MakeingPaymentAsync(Items, Payment))
                return View("ErrView");
            for (var i = 0; i < Request.Cookies.Keys.Count; i++)
            {
                try
                {
                    Delete(i.ToString());
                }
                catch
                {
                    continue;
                }
            }
            return RedirectToAction("Index", "Product");
        }

        public IActionResult ErrView()
        {
            return View();
        }

        private string Get(string cName) =>
            Request.Cookies[cName];

        private void Delete(string cName) =>
            Response.Cookies.Delete(cName);
    }
}