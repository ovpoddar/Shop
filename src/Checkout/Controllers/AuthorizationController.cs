using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.ViewModels;

namespace Checkout.Controllers
{
    public class AuthorizationController : Controller
    {
        [HttpGet]
        [Type]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model, string con, string ac)
        {
            if (ModelState.IsValid)
            {
                //var result = await _signManager.LoginAsyncMethod(model, model.Password);
                //if (result.Success)
                //    return RedirectToAction(ac, con);
                //foreach (var err in result.Error)
                //{
                //    ModelState.AddModelError(string.Empty, err);
                //}
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            //if (await _validator.IsMember())
            //    return RedirectToAction("Index", "Product");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var result = await _userHandler.CreateUserAsync(model);
                //if (result.Success)
                //    return RedirectToAction("LogIn");
                //foreach (var error in result.Error)
                //{
                //    ModelState.AddModelError(string.Empty, error);
                //}
            }
            return View(model);
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class TypeAttribute : Attribute
    {
        public TypeAttribute()
        {

        }
        public bool isenable(HttpContext httpContext)
        {
            var c = httpContext.Request.Cookies["user"];
            return !string.IsNullOrWhiteSpace(c);
        }
    }
}
