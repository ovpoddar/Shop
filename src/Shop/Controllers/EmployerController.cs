using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Handlers.Interfaces;

namespace Shop.Controllers
{
    public class EmployerController : Controller
    {
        private readonly IEmployerHandler _employerHandler;

        public EmployerController(IEmployerHandler employerHandler)
        {
            _employerHandler = employerHandler ?? throw new ArgumentNullException(nameof(_employerHandler));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_employerHandler.GetAll());
        }

        [HttpPost]
        public IActionResult IndexAsync(string Username)
        {
            _employerHandler.BlockEmployer(Username);
            return View(_employerHandler.GetAll());
        }
    }
}
