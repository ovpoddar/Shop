using Checkout.Managers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Checkout.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IloginManager  _iloginManager;

        public AuthenticationController(IloginManager iloginManager)
        {
            _iloginManager = iloginManager ?? throw new ArgumentNullException(nameof(_iloginManager));
        }
        [HttpGet("Checkout/Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("Checkout/Login")]
        public async Task<IActionResult> LoginAsync(LogInViewModel logInView)
        {
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
        public async Task<IActionResult> LogOutAsync()
        {
            await _iloginManager.LogMeOutAsync();
            return RedirectToAction("Login");
        }
    }
}
