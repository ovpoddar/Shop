using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Handlers;
using Checkout.Managers;
using Checkout.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Shop.Handlers.Interfaces;
using Shop.Models;

namespace Checkout.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IItemManager _itemManager;
        private readonly IItemHandler<ItemModel> _itemHandler;
        private readonly IPaymentManager _paymentManager;

        public CheckoutController(IItemManager item, IItemHandler<ItemModel> itemHandler, IPaymentManager paymentManager)
        {
            _itemManager = item ?? throw new ArgumentNullException(nameof(_itemManager));
            _itemHandler = itemHandler ?? throw new ArgumentNullException(nameof(_itemHandler));
            _paymentManager = paymentManager ?? throw new ArgumentNullException(nameof(_paymentManager));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(ItemViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            await _itemManager.Add(model);
            return View();
        }

        public IActionResult Delete(int id)
        {
            _itemManager.remove(id);
            return Redirect("../Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(string Name)
        {
            return View(await _itemManager.Model(Name));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(ItemViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            await _itemManager.Add(model);
            return Redirect("Index");
        }

        public async Task<IActionResult> PaymentAsync(uint Payment)
        {
            if (!await _paymentManager.MakeingPurchaseAsync(_itemHandler.List, Payment))
                return View("ErrView");
            return Redirect("https://localhost:44350/");
        }
    }
}
