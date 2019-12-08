using Microsoft.AspNetCore.Mvc;
using Shop.Managers;
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
            var model = _productManager.GetModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int categoryId)
        {
            var model = _productManager.GetFilteredModel(categoryId);
            return View(model);
        }
    }
}
