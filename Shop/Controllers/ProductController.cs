using Microsoft.AspNetCore.Mvc;
using Shop.Managers;
using System;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManager ProductManager;
        public ProductController(IProductManager productManager)
        {
            ProductManager = productManager ?? throw new ArgumentNullException(nameof(ProductManager));
        }


        [HttpGet]
        public IActionResult Index()
        {
            var model = ProductManager.GetModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(int categoryId)
        {
            var model = ProductManager.GetFilteredModel(categoryId);
            return View(model);
        }
    }
}
