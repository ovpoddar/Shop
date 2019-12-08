using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using Shop.ViewModels;
using System;

namespace Shop.Controllers
{
    public class WholeSaleController : Controller
    {
        private readonly IWholesaleHandler _wholesaleHandler;

        public WholeSaleController(IWholesaleHandler wholeSaleManager) =>
            _wholesaleHandler = wholeSaleManager ?? throw new ArgumentNullException(nameof(_wholesaleHandler));
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(WholeSaleViewModel model)
        {
            var result = _wholesaleHandler.Add(model);
            if (ModelState.IsValid || result)
                return RedirectToAction("Index", "Product");
            return View();
        }
    }
}
