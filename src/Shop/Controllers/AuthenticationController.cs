using Microsoft.AspNetCore.Mvc;
using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IEmployerHandler _employerHandler;
        private readonly IAuthenticationManager _authenticationManager;

        public AuthenticationController(IEmployerHandler employerHandler, IAuthenticationManager authenticationManager)
        {
            _employerHandler = employerHandler ?? throw new ArgumentNullException(nameof(_employerHandler));
            _authenticationManager = authenticationManager ?? throw new ArgumentNullException(nameof(_authenticationManager));
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _employerHandler.GetEmployer(model.UserName);
                if (user != null)
                    if ((await _authenticationManager.LogInUserAsync(user, model.Password)).Succeeded)
                        return RedirectToAction("index", "product");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationManager.SignUpUserAsync(model);
                if (result.Success)
                    return Redirect($"{WebSitesUrls.CallingPoient}");
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOutAsync()
        {
            await _authenticationManager.SignOutUserAsync();
            return RedirectToAction("LogIn", "Authentication");
        }
    }
}