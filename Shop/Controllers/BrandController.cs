using Microsoft.AspNetCore.Mvc;
using Shop.Entities;
using Shop.Handlers;
using System;

namespace Shop.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandHandler _brandHandler;

        public BrandController(IBrandHandler brandHandler) =>
            _brandHandler = brandHandler ?? throw new ArgumentNullException(nameof(brandHandler));
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Brand brand)
        {
            return !ModelState.IsValid || brand.BrandName == null
                ? View(brand)
                : _brandHandler.AddBrand(brand) ? RedirectToAction("Index", "Product") : (IActionResult)View(brand);
        }
    }
}
