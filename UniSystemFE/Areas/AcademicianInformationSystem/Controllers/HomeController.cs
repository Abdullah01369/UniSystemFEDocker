using Microsoft.AspNetCore.Mvc;

namespace UniSystemFE.Areas.AcademicianInformationSystem.Controllers
{
    [Area("AcademicianInformationSystem")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
