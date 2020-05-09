using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using Shop.Managers;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentManager _manager;
        private readonly ICookieHandler _cookie;
        private readonly IValidatorHandler _validator;

        public PaymentController(IPaymentManager manager, ICookieHandler cookie, IValidatorHandler validator)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(_manager));
            _cookie = cookie ?? throw new ArgumentNullException(nameof(_cookie));
            _validator = validator ?? throw new ArgumentNullException(nameof(_validator));
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!await _validator.IsMember())
                return RedirectToAction("LogIn", "Authentication");
            if (_cookie.GetAll().Count == 1)
                return RedirectToAction("Index", "Checkout");
            var model = new PaymentViewModel()
            {
                Items = new List<ItemModel>(),
                Total = 0
            };
            for (var i = 0; i < _cookie.GetAll().Count; i++)
            {
                try
                {
                    var cookie = _cookie.Get(i.ToString()).Split("=");
                    model.Items.Add(_manager.CreateModel(cookie[0], cookie[1], cookie[2], cookie[3], cookie[4], cookie[5]));
                    model.Total += decimal.Parse(cookie[5]);
                }
                catch
                {
                    continue;
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(List<ItemModel> Items, uint Payment)
        {
            if (!await _manager.MakeingPaymentAsync(Items, Payment))
                return View("ErrView");
            for (var i = 0; i < _cookie.GetAll().Count; i++)
            {
                try
                {
                    _cookie.Delete(i.ToString());
                }
                catch
                {
                    continue;
                }
            }
            return RedirectToAction("Index", "Product");
        }

        public IActionResult ErrView()
        {
            return View();
        }
    }
}