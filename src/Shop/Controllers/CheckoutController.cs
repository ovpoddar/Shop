using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using Shop.Managers;
using Shop.ViewModels;
using System;
using System.Collections.Generic;

namespace Shop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IItemManager _item;
        private readonly IProductHandler _product;

        public CheckoutController(IItemManager item, IProductHandler product)
        {
            _item = item ?? throw new System.ArgumentNullException(nameof(_item));
            _product = product ?? throw new System.ArgumentNullException(nameof(_product));
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(ItemViewModel model)
        {
            if (!ModelState.IsValid)
                return View();
            _item.add(model);
            return View();
        }
        public IActionResult Delete(int id)
        {
            _item.remove(id);
            return Redirect("../Index");
        }

        [HttpGet]
        public IActionResult Update(string Name)
        {
            var model = _item.model(Name);
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(ItemViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            _item.add(model);
            return Redirect("Index");
        }

        public IActionResult Payment(List<string> names, List<int> Quantitys)
        {
            CookieOptions cookie = new CookieOptions()
            {
                Domain = "localhost",
                Expires = DateTime.Now.AddMonths(2),
                HttpOnly = false,
                IsEssential = true,
                Path = "/",
                SameSite = SameSiteMode.Lax,
                Secure = true
            };
            Response.Cookies.Append("xx", "xx", cookie);
            //for (var i = 0; i < names.Count; i++)
            //{
            //    var val = names[i] + "+" + Quantitys[i];
            //    Set("list", val, 20);
            //}
            // store them into cookie
            return View();
        }
    }
}