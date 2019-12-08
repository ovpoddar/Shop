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

        [AcceptVerbs("post")]
        public IActionResult Index(int categoryId, int PageNumber)
        {
            var i = PageNumber + 1;
            switch (categoryId)
            {
                case 0:
                    {
                        var model = _productManager.GetModel(i);
                        return View(model);
                    }

                default:
                    {
                        var model = _productManager.GetFilteredModel(categoryId, i);
                        return View(model);
                    }
            }

        }
    }
}
