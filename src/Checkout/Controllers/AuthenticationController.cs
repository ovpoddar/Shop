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
                (await _requestManger.PostRequest($"{WebSitesUrls.EndPoient}api/Authentication/Login", logInView, "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJBbWFyIFBvZGRhciIsImVtYWlsIjoiYW1hcnBvZGRlcjBAZ21haWwuY29tIiwiZ2VuZGVyIjoiTWFsZSIsImV4cCI6MTU5NDEwMTc1OCwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFtYXJwb2RkYXIiLCJpYXQiOiI0YjgwMjA4MC1kNGVmLTQ2YWYtYjc4NS0yZjAyMTQwZWNlYzAiLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjU5NjE2LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzUwLyJ9.GBCtVTgO6QxuaS__JTIjFGwHT3C7pychjwhOvapt7R1SGV5uQazHCek-NAQj0-5tYViv2eh4tDIP9fY5hXhwQw"));
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
