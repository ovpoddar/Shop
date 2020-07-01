using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Shop.Handlers;
using Shop.Handlers.Interfaces;

namespace Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployerController : Controller
    {
        private readonly IEmployerHandler _employerHandler;
        private readonly UserManager<Employer> _userManager;

        public EmployerController(IEmployerHandler employerHandler, UserManager<Employer> userManager)
        {
            _employerHandler = employerHandler ?? throw new ArgumentNullException(nameof(_employerHandler));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(_userManager));
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var model = new List<Employer>();
            foreach(var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                    continue;
                model.Add(user);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(string Username)
        {
            await _employerHandler.BlockEmployerAsync(Username);
            return View(_userManager.Users.ToList());
        }
    }
}
