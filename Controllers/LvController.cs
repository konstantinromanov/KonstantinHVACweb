using KonstantinHVACweb.BusinessLogic.Models;
using KonstantinHVACweb.BusinessLogic.Services.Interface;
using KonstantinHVACweb.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace KonstantinHVACweb.Controllers
{
    public class LvController : Controller
    {
        private readonly IUserService _iUserIdentityService;

        private new UserModel User => _iUserIdentityService.User;

        public LvController(IUserService iUserIdentityService)
        {
            _iUserIdentityService = iUserIdentityService;
        }

        public IActionResult Index()
        {
            return View($"~/Views/Lv/Index.cshtml");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View($"~/Views/Lv/About.cshtml");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View($"~/Views/Lv/Contact.cshtml");
        }

        public IActionResult Services()
        {
            ViewData["Message"] = "Your Services page.";

            return View($"~/Views/Lv/Services.cshtml");
        }

        public IActionResult Projects()
        {
            ViewData["Message"] = "Your Project's page.";

            return View($"~/Views/Lv/Projects.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string errorCode)
        {
            return View($"~/Views/Lv/Error.cshtml", new ErrorViewModel
            {
                ErrorCode = errorCode
            });
        }

        [HttpPost]
        public JsonResult ChangeLanguage(string languageIsoCode)
        {
            User.Language = Language.GetLanguageByIsoCode(languageIsoCode);

            return Json(new BasicResponseViewModel("Language saved.", true));
        }
    }
}


