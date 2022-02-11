using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Managers;
using Shop.Managers.Interfaces;
using Shop.Utilities;
using System;
using System.Linq;

namespace Shop.Controllers
{
    [AuthorizeCookie]
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly IGenericRepository<Product> _repository;
        public ProductController(IProductManager productManager, IGenericRepository<Product> repository)
        {
            _productManager = productManager ?? throw new ArgumentNullException(nameof(ProductManager));
            this._repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var c = _repository.GetAll().Sum(a =>a.Id);
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
