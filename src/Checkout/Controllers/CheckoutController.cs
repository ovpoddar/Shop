using Checkout.Handlers;
using Checkout.Managers;
using Checkout.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using System;
using System.Threading.Tasks;

namespace Checkout.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IItemManager _itemManager;
        private readonly IItemHandler<ItemModel> _itemHandler;
        private readonly IPurchaseHandler _purchaseHandler;
        private readonly IUserHandler _userHandler;

        public CheckoutController(IItemManager item, IItemHandler<ItemModel> itemHandler, IPurchaseHandler purchaseHandler, IUserHandler userHandler)
        {
            _itemManager = item ?? throw new ArgumentNullException(nameof(_itemManager));
            _itemHandler = itemHandler ?? throw new ArgumentNullException(nameof(_itemHandler));
            _purchaseHandler = purchaseHandler ?? throw new ArgumentNullException(nameof(_purchaseHandler));
            _userHandler = userHandler ?? throw new ArgumentNullException(nameof(_userHandler));
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (string.IsNullOrWhiteSpace(_userHandler.UserToken))
                return RedirectToAction("Login", "Checkout");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(ItemViewModel model)
        {
            if (string.IsNullOrWhiteSpace(_userHandler.UserToken))
                return RedirectToAction("Login", "Checkout");
            if (string.IsNullOrWhiteSpace(_userHandler.UserToken))
                return RedirectToAction("Login", "Checkout");
            var result = await _itemManager.Add(model, _userHandler.UserToken);
            if (ModelState.IsValid && string.IsNullOrEmpty(result))
                return View(model);
            ModelState.AddModelError("1", result);
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (string.IsNullOrWhiteSpace(_userHandler.UserToken))
                return RedirectToAction("Login", "Checkout");
            _itemManager.remove(id);
            return Redirect("../Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(string Name)
        {
            if (string.IsNullOrWhiteSpace(_userHandler.UserToken))
                return RedirectToAction("Login", "Checkout");
            return View(await _itemManager.Model(Name, _userHandler.UserToken));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(ItemViewModel model)
        {
            if (string.IsNullOrWhiteSpace(_userHandler.UserToken))
                return RedirectToAction("Login", "Checkout");
            var result = await _itemManager.Add(model, _userHandler.UserToken);
            if (!ModelState.IsValid && string.IsNullOrWhiteSpace(result))
                return Redirect("Index");
            ModelState.AddModelError("1", result);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Payment(uint Payment)
        {
            if (string.IsNullOrWhiteSpace(_userHandler.UserToken))
                return RedirectToAction("Login", "Checkout");
            var responce = await _purchaseHandler.MakePurchaseCallAsync(new PurchaseModel()
            {
                Items = _itemHandler.List,
                PaymentType = Payment
            }, _userHandler.UserToken);
            if (!responce.Success)
                return View(responce.Objects);
            _itemHandler.List.Clear();
            return Redirect(WebSitesUrls.CallingPoient);
        }

    }
}
