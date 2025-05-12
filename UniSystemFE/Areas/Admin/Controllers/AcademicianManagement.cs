using Microsoft.AspNetCore.Mvc;

namespace UniSystemFE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AcademicianManagement : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
