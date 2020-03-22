using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using Shop.ViewModels;
using System;

namespace Shop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryHandler _categoryHandler;

        public CategoryController(ICategoryHandler categoryHandler) =>
            _categoryHandler = categoryHandler ?? throw new ArgumentNullException(nameof(categoryHandler));

        [HttpGet]
        public IActionResult Index()
        {
            var categories = new CategoryViewModel
            {
                Categories = _categoryHandler.GetAll()
            };
            return View(categories);
        }

        [HttpPost]
        public IActionResult Index(CategoryViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                model.Categories = _categoryHandler.GetAll();
                return View(model);
            }
            _categoryHandler.AddCategory(model);
            return RedirectToAction("Index", "Product");
        }
    }
}
