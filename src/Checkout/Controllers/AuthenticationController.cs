using Checkout.Managers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IRequestManger _requestManger;

        public AuthenticationController(IRequestManger requestManger)
        {
            _requestManger = requestManger ?? throw new ArgumentNullException(nameof(_requestManger));
        }
        [HttpGet("Checkout/Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("Checkout/Login")]
        public async Task<IActionResult> LoginAsync(LogInViewModel logInView)
        {
            var result = JsonConvert.DeserializeObject<Results<CustomeSignInResult>>
                (await _requestManger.PostRequest($"{WebSitesUrls.EndPoient}api/Authentication/Login", logInView));
            if (result.Result.Succeeded)
                return RedirectToAction("Index", "Checkout");
            ModelState.AddModelError("", result.Exception);
            foreach (var err in result.Result.Errors)
            {
                ModelState.AddModelError(err.Code, err.Description);
            }
            return View(logInView);
        }
    }
}
