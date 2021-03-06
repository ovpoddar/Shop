﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Managers;
using Shop.Managers.Interfaces;
using Shop.Utilities;
using System;

namespace Shop.Controllers
{
    [AuthorizeCookie]
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;
        public ProductController(IProductManager productManager) =>
            _productManager = productManager ?? throw new ArgumentNullException(nameof(ProductManager));


        [HttpGet]
        public IActionResult Index()
        {
            return View(_productManager.GetModel(1));
        }

        [HttpPost]
        public IActionResult Index(int categoryId, int pageNumber)
        {
            return View(categoryId == 0 ?
                _productManager.GetModel(pageNumber) :
                _productManager.GetFilteredModel(categoryId, pageNumber));
        }
        [AllowAnonymous]
        public IActionResult MissingPage()
        {
            return View();
        }
    }
}
