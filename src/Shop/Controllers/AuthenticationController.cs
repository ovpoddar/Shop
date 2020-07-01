using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using Shop.Handlers.Interfaces;
using Shop.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<Employer> _userManager;
        private readonly SignInManager<Employer> _signInManager;
        private readonly IEmployerHandler _employeeReposotory;

        public AuthenticationController(UserManager<Employer> userManager, SignInManager<Employer> signInManager, IEmployerHandler employeeReposotory)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(_userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(_signInManager));
            _employeeReposotory = employeeReposotory ?? throw new ArgumentNullException(nameof(_employeeReposotory));
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
                var user = await _userManager.FindByEmailAsync(model.UserName);
                if (user == null)
                    user = _userManager.Users.Where(e => e.UserName == model.UserName || e.PhoneNumber == model.UserName).FirstOrDefault();
                if (user == null)
                    return View(model);
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                if (result.Succeeded && _employeeReposotory.IsAccessable(user))
                {
                    var check = await _employeeReposotory.LastcheckInAsync(user);
                    if (check.Succeeded)
                        return RedirectToAction("index", "product");
                }
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
                var user = new Employer()
                {
                    UserName = model.UserName,
                    PhoneNumber = model.MobileNo.ToString(),
                    Email = model.Email,
                    City = model.City,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Gender = model.Gender.ToString(),
                    Active = true,
                    LastLogin = DateTime.UtcNow
                };
                var result = await _userManager.CreateAsync(user, model.ConfirmPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("index", "product");
                }
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LogIn", "Authentication");
        }
    }
}