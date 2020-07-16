using Checkout.Handlers;
using Checkout.Managers;
using Microsoft.AspNetCore.Mvc;
using Shop.ViewModels;
using System;
using System.Threading.Tasks;

namespace Checkout.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IloginManager _iloginManager;
        private readonly IUserHandler _userHandler;

        public AuthenticationController(IloginManager iloginManager, IUserHandler userHandler)
        {
            _iloginManager = iloginManager ?? throw new ArgumentNullException(nameof(_iloginManager));
            _userHandler = userHandler ?? throw new ArgumentNullException(nameof(_userHandler));
        }
        [HttpGet("Checkout/Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("Checkout/Login")]
        public async Task<IActionResult> LoginAsync(LogInViewModel logInView)
        {
            if (string.IsNullOrWhiteSpace(_userHandler.UserToken))
                RedirectToAction("Index", "Checkout");
            if (ModelState.IsValid)
            {
                var result = await _iloginManager.LogMeIn(logInView);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Checkout");
                }
                foreach (var err in result.Error)
                {
                    ModelState.AddModelError("", err);
                }
            }
            return View(logInView);
        }

        [HttpPost("Checkout/LogOut")]
        public IActionResult LogOut()
        {
            _iloginManager.LogMeOutAsync();
            return RedirectToAction("Login");
        }
    }
}
