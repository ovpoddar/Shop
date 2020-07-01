using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Handlers.Interfaces;
using Shop.Utilities;
using System;

namespace Shop.Controllers
{
    [Authorize(Roles = "Admin")]
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
