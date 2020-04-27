using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using Shop.ViewModels;

namespace Shop.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            var models = new List<ItemViewModel>();
            if (Request.Cookies.Keys.Count == 1)
                return RedirectToAction("Index", "Checkout");
            for (var i = 0; i < Request.Cookies.Keys.Count; i++)
            {
                try
                {
                    var cookie = Get(i.ToString()).Split("=");
                    models.Add(new ItemViewModel
                    {
                        Name = cookie[0],
                        Quantity = int.Parse(cookie[1])
                    });
                }
                catch
                {
                    continue;
                }
            }
            var x = models;
            return View();
        }


        public string Get(string cName) =>
            Request.Cookies[cName];
    }
}