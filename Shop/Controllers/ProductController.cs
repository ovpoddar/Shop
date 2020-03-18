using Microsoft.AspNetCore.Mvc;
using Shop.Managers;
using Shop.ViewModels;
using System;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;
        public ProductController(IProductManager productManager) =>
            _productManager = productManager ?? throw new ArgumentNullException(nameof(ProductManager));


        [HttpGet]
        public IActionResult Index()
        {
            var model = _productManager.GetModel(1);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int categoryId, int pageNumber)
        {
            var model = categoryId == 0 ? _productManager.GetModel(pageNumber) : _productManager.GetFilteredModel(categoryId, pageNumber);
            return View(model);
        }

    }
}
