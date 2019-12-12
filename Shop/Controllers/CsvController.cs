using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class CsvController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(CsvViewModel model)
        {
            if (ModelState.IsValid)
            {
                var x = model.Csv.FileName.Length;
            };
            return View();
        }
    }
}
