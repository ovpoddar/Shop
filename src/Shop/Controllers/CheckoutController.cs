using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using Shop.Managers;
using Shop.ViewModels;
using System.Collections.Generic;
using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;

namespace Shop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IItemManager _item;
        private readonly IProductHandler _product;
        private readonly ICookieHandler _cookie;

        public CheckoutController(IItemManager item, IProductHandler product, ICookieHandler cookie)
        {
            _item = item ?? throw new System.ArgumentNullException(nameof(_item));
            _product = product ?? throw new System.ArgumentNullException(nameof(_product));
            _cookie = cookie ?? throw new System.ArgumentNullException(nameof(_cookie));
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
            return View(_item.Model(Name));
        }
        [HttpPost]
        public IActionResult Update(ItemViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            _item.add(model);
            return Redirect("Index");
        }

        public IActionResult Payment(List<string> id, List<string> name, List<string> brand, List<string> quantity, List<string> price, List<string> totalPrice)
        {
            for (var i = 0; i < id.Count; i++)
            {
                _cookie.Create(i.ToString(), $"{id[i]}={name[i]}={brand[i]}={quantity[i]}={price[i]}={totalPrice[i]}");
            }
            return RedirectToAction("Index", "Payment");
        }
    }
}