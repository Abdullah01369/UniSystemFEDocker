using Microsoft.AspNetCore.Mvc;
using ServiceLayer.AbstractsModel;
using SharedLayer.ViewModels;

namespace UniSystemFE.Areas.AcademicianInformationSystem.Controllers
{
    [Area("AcademicianInformationSystem")]
    public class ResearcherController : Controller
    {

        private readonly IResearcherService _researcherService;
        private readonly IConfiguration _config;

        public static string ResearcherName;
        public static string ResearcherEmail;

        public ResearcherController(IResearcherService researcherService, IConfiguration config)
        {
            _researcherService = researcherService;
            _config = config;
        }
        public IActionResult Index()
        {
            var baseUrl = _config["BaseUrl"];
            ViewBag.BaseUrl = baseUrl;
            return View();
        }
        public async Task<IActionResult> Information(string email)
        {

            if (email!=null)
            {
                ResearcherEmail = email;
            }
           

            var val = await _researcherService.ResearcherInfo(ResearcherEmail);
            var metrics = await _researcherService.GetMetrics(ResearcherEmail);

            ResearcherMainViewModel vm = new ResearcherMainViewModel
            {
                ResearchMetricDto = metrics.Data,
                ResearcherInfoDto = val.Data
            };

            if (val.StatusCode != 200)
            {

                TempData["Fail"] = "Başarısız";
                return View();
            }
            ResearcherName = val.Data.Name + " " + val.Data.Surname.ToUpper();
            TempData["ResearcherNameSurname"] = val.Data.Name + " " + val.Data.Surname.ToUpper();
            return View(vm);
        }
        public async Task<IActionResult> AcademicInformation()
        {
      
            var val = await _researcherService.GetEducInfo(ResearcherEmail);
            if (val.StatusCode != 200)
            {

                TempData["Fail"] = "Başarısız";
                return View();
            }
            TempData["ResearcherNameSurname"] = ResearcherName;
            return View(val.Data);
        }
        public async Task<IActionResult> WorkAreas()
        {

            var val = await _researcherService.GetResearcherAreaInfo(ResearcherEmail);

            if (val.StatusCode != 200)
            {
                TempData["Result"] = "Bir Problemle Karşılaşıldı";
                return View();
            }
            TempData["ResearcherNameSurname"] = ResearcherName;

            return View(val.Data);
        }
        public async Task<IActionResult> Experiments()
        {


            var val = await _researcherService.GetResearcherExp(ResearcherEmail);
            TempData["ResearcherNameSurname"] = ResearcherName;

            return View(val.Data);
        }
        public async Task<IActionResult> Publication()
        {


            var val = await _researcherService.GetPublication(ResearcherEmail);
            TempData["ResearcherNameSurname"] = ResearcherName;

            return View(val.Data);
        }
        public IActionResult PatientProjects()
        {
            return View();
        }
        public IActionResult ScienceWorks()
        {
            return View();
        }
        public IActionResult Achievement()
        {
            return View();
        }
        public IActionResult Announcement()
        {
            return View();
        }
        public IActionResult DownloadCV()
        {
            return View();
        }
        public async Task<IActionResult> Contact()
        {

            var val = await _researcherService.GetContact(ResearcherEmail);
            TempData["ResearcherNameSurname"] = ResearcherName;

            return View(val.Data);
        }
    }
}
