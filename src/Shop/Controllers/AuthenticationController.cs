//using Microsoft.AspNetCore.Mvc;
//using Shop.Handlers.Interfaces;
//using Shop.Managers.Interfaces;
//using Shop.ViewModels;
//using System;
//using System.Threading.Tasks;

//namespace Shop.Controllers
//{
//    public class AuthenticationController : Controller
//    {

//        [HttpGet]
//        public async Task<IActionResult> LogIn(string con, string ac)
//        {
//            if (await _validator.IsMember())
//                return RedirectToAction("Index", "Product");
//            ViewBag.con = con;
//            ViewBag.ac = ac;
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> LogIn(LogInViewModel model, string con, string ac)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _signManager.LoginAsyncMethod(model, model.Password);
//                if (result.Success)
//                    return RedirectToAction(ac, con);
//                foreach (var err in result.Error)
//                {
//                    ModelState.AddModelError(string.Empty, err);
//                }
//            }
//            return View(model);
//        }

//        [HttpGet]
//        public async Task<IActionResult> SignIn()
//        {
//            if (await _validator.IsMember())
//                return RedirectToAction("Index", "Product");
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> SignIn(SignInViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _userHandler.CreateUserAsync(model);
//                if (result.Success)
//                    return RedirectToAction("LogIn");
//                foreach (var error in result.Error)
//                {
//                    ModelState.AddModelError(string.Empty, error);
//                }
//            }
//            return View(model);
//        }

//        [HttpGet]
//        public IActionResult LogOut(string con, string ac)
//        {
//            _signManager.LogOut();
//            return RedirectToAction(ac, con);
//        }
//    }
//}