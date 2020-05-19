using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using Shop.Managers;
using Shop.ViewModels;
using System;
using System.Threading.Tasks;
using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;

namespace Shop.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly ISignManager _signManager;
        private readonly IValidatorHandler _validator;

        public AuthenticationController(IUserManager userManager, ISignManager signManager, IValidatorHandler validator)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(_userManager));
            _signManager = signManager ?? throw new ArgumentNullException(nameof(_signManager));
            _validator = validator ?? throw new ArgumentNullException(nameof(_validator));
        }
        [HttpGet]
        public async Task<IActionResult> LogIn(string con, string ac)
        {
            if (await _validator.IsMember())
                return RedirectToAction("Index", "Product");
            ViewBag.con = con;
            ViewBag.ac = ac;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model, string con, string ac)
        {
            if (ModelState.IsValid)
            {
                var result = await _signManager.LoginAsyncMethod(model, model.Password);
                if (result.Success)
                    return RedirectToAction(ac, con);
                foreach (var err in result.Error)
                {
                    ModelState.AddModelError(string.Empty, err);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            var membercheck = await _validator.IsMember();
            if (membercheck)
                return RedirectToAction("Index", "Product");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateUserAsync(model);
                if (result.Success)
                    return RedirectToAction("LogIn");
                foreach (var error in result.Error)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult LogOut(string con, string ac)
        {
            _signManager.LogOut();
            return RedirectToAction(ac, con);
        }
    }
}