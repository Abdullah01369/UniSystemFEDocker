using Microsoft.AspNetCore.Mvc;

namespace UniSystemFE.Areas.Student.Controllers
{
    [Area("Student")]
    public class MetaverseController : Controller
    {
        public IActionResult Metaverse()
        {
            return View();
        }
    }
}
