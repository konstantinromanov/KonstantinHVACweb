using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KonstantinHVACweb.Models;

namespace KonstantinHVACweb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/lv/Home/Index.cshtml");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View("~/Views/lv/Home/About.cshtml");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View("~/Views/lv/Home/Contact.cshtml");
        }

        public IActionResult Services()
        {
            ViewData["Message"] = "Your Services page.";

            return View("~/Views/lv/Home/Services.cshtml");
        }

        public IActionResult Projects()
        {
            ViewData["Message"] = "Your Project's page.";

            return View("~/Views/lv/Home/Projects.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string errorCode)
        {
            return View(new ErrorViewModel 
            {
                ErrorCode = errorCode
            });
        }
    }
}


