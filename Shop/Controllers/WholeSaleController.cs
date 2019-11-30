using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using Shop.ViewModels;
using System;

namespace Shop.Controllers
{
    public class WholeSaleController : Controller
    {
        private readonly IWholesaleHandler wholesaleHandler;

        public WholeSaleController(IWholesaleHandler wholeSaleManager)
        {
            wholesaleHandler = wholeSaleManager ?? throw new ArgumentNullException(nameof(wholesaleHandler));
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = wholesaleHandler.GetModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(WholeSaleViewModel model, int selected)
        {
            var backupmodel = wholesaleHandler.GetModel();
            var result = wholesaleHandler.Add(model, selected);
            if (ModelState.IsValid || result)
                return RedirectToAction("Index", "Product");
            return View(backupmodel);
        }
    }
}
