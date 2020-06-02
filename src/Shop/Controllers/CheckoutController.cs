using Microsoft.AspNetCore.Mvc;
using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IItemManager _item;
        private readonly IPaymentManager _manager;
        private readonly IValidatorHandler _validator;

        public CheckoutController(IItemManager item, IPaymentManager manager, IValidatorHandler validator)
        {
            _item = item ?? throw new ArgumentNullException(nameof(_item));
            _manager = manager ?? throw new ArgumentNullException(nameof(_manager));
            _validator = validator ?? throw new ArgumentNullException(nameof(_validator));
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!await _validator.IsMember())
                return RedirectToAction("LogIn", "Authentication");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ItemViewModel model)
        {

            if (!await _validator.IsMember())
                return RedirectToAction("LogIn", "Authentication");
            if (!ModelState.IsValid)
                return View();
            _item.add(model);
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _validator.IsMember())
                return RedirectToAction("LogIn", "Authentication");
            _item.remove(id);
            return Redirect("../Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(string Name)
        {
            if (!await _validator.IsMember())
                return RedirectToAction("LogIn", "Authentication");
            return View(_item.Model(Name));
        }
        [HttpPost]
        public async Task<IActionResult> Update(ItemViewModel model)
        {
            if (!await _validator.IsMember())
                return RedirectToAction("LogIn", "Authentication");
            if (!ModelState.IsValid)
                return View(model);
            _item.add(model);
            return Redirect("Index");
        }

        public async Task<IActionResult> PaymentAsync(List<string> id, List<string> name, List<string> brand, List<string> quantity, List<string> price, List<string> totalPrice, uint Payment)
        {
            if (!await _validator.IsMember())
                return RedirectToAction("LogIn", "Authentication");
            if (!await _manager.MakeingPurchaseAsync(_item.CreateItemModels(id, name, brand, quantity, price, totalPrice), Payment))
                  return View("ErrView");
            return RedirectToAction("Index", "Product");
        }

        public IActionResult ErrView()
        {
            return View();
        }
    }
}