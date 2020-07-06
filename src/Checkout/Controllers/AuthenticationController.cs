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
                (await _requestManger.PostRequest($"{WebSitesUrls.EndPoient}api/Authentication/Login", logInView, "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJBbWFyIFBvZGRhciIsImVtYWlsIjoiYW1hcnBvZGRlcjBAZ21haWwuY29tIiwiZ2VuZGVyIjoiTWFsZSIsImV4cCI6MTU5NDA5MzA3NCwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFtYXJwb2RkYXIiLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjU5NjE2LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzUwLyJ9.cn3mkaTx9Yueat9RVOyJx7MBQasyXP3xhwmXaeVR9b9AW-mnPr3Cmj6hu2ESQ8W6zI5wO_MN5R6QpombZKimsg"));
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
