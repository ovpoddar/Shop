using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Handlers;
using Checkout.Managers;
using Checkout.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Handlers.Interfaces;
using Shop.Models;

namespace Checkout.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IItemManager _itemManager;
        private readonly IItemHandler<ItemModel> _itemHandler;
        private readonly IPurchaseHandler _purchaseHandler;

        public CheckoutController(IItemManager item, IItemHandler<ItemModel> itemHandler, IPurchaseHandler purchaseHandler)
        {
            _itemManager = item ?? throw new ArgumentNullException(nameof(_itemManager));
            _itemHandler = itemHandler ?? throw new ArgumentNullException(nameof(_itemHandler));
            _purchaseHandler = purchaseHandler ?? throw new ArgumentNullException(nameof(_purchaseHandler));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(ItemViewModel model)
        {
            var result = await _itemManager.Add(model);
            if (ModelState.IsValid && string.IsNullOrEmpty(result))
                return View(model);
            ModelState.AddModelError("1", result);
            return View(model);
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
            var result = await _itemManager.Add(model);
            if (!ModelState.IsValid && string.IsNullOrWhiteSpace(result)) 
                return Redirect("Index");
            ModelState.AddModelError("1", result);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Payment(uint Payment)
        {
            var responce = await _purchaseHandler.MakePurchaseCallAsync(new PurchaseModel()
            {
                Items = _itemHandler.List,
                PaymentType = Payment
            });
            if (!responce.Success)
                return View(responce.Objects);
            return Redirect(WebSitesUrls.EndPoient);
        }

    }
}
