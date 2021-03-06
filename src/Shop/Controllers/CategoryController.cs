﻿using Microsoft.AspNetCore.Mvc;
using Shop.Handlers.Interfaces;
using Shop.Utilities;
using Shop.ViewModels;
using System;
using System.Linq;

namespace Shop.Controllers
{
    [AuthorizeCookie]
    public class CategoryController : Controller
    {
        private readonly ICategoryHandler _categoryHandler;

        public CategoryController(ICategoryHandler categoryHandler) =>
            _categoryHandler = categoryHandler ?? throw new ArgumentNullException(nameof(categoryHandler));

        [HttpGet]
        public IActionResult Index()
        {
            return View(new CategoryViewModel
            {
                Categories = _categoryHandler.GetAll()
            });
        }

        [HttpPost]
        public IActionResult Index(CategoryViewModel model)
        {
            var result = _categoryHandler.AddCategory(model);
            if (result.Success)
                return RedirectToAction("Index", "Product");

            model.Categories = result.Result.ToList();
            return View(model);
        }
    }
}