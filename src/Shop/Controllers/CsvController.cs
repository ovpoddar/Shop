using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Managers.Interfaces;
using Shop.ViewModels;
using System;

namespace Shop.Controllers
{
    [Authorize]
    public class CsvController : Controller
    {
        private readonly ICsvManager _csvManager;

        public CsvController(ICsvManager csvManager) => _csvManager = csvManager ?? throw new ArgumentNullException(nameof(_csvManager));


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CsvViewModel model)
        {
            if (!ModelState.IsValid || model.Csv == null)
                return View();
            var result = _csvManager.Upload(model);
            if (!result.Success)
                return View();
            _csvManager.Update(result.Path);
            return RedirectToAction("index", "product");
        }
    }
}
