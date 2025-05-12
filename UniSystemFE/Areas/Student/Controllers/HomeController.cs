using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.AbstractsModel;
using ServiceLayer.Requests;
using ServiceLayer.Services;
using ServiceLayer.ViewModels;
using SharedLayer.Dtos;

namespace UniSystemFE.Areas.Student.Controllers
{
    [Area("Student")]

    public class HomeController : Controller
    {
        private readonly IAnnouncementService _announcementService;
        private readonly ILessonServices _lessonServices;
        private readonly IAccountServices _accountServices;
        private readonly IAcademicYearServices _AcademicYearServices;

        public HomeController(IAnnouncementService announcementService, ILessonServices lessonServices, IAccountServices account, IAcademicYearServices academicYearServices)
        {
            _announcementService = announcementService;
            _lessonServices = lessonServices;
            _accountServices = account;
            _AcademicYearServices = academicYearServices;
        }


        public async Task<IActionResult> Index()
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);

            var val = await _announcementService.GetAll(userInfo.Email);
            return View(val);
        }
        [HttpGet]
        public async Task<IActionResult> DownladLessonProgram()
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);



            var code = await _lessonServices.TakeDepartmentCode(userInfo.UserName, userInfo.Email);
            if (string.IsNullOrEmpty(code))
            {
                return StatusCode(500, "PDF indirme sırasında bir hata oluştu.");

            }


            try
            {
                var pdfBytes = await _lessonServices.DownloadLessonProgramAsync(code);

                return File(pdfBytes, "application/pdf", $"{code}.pdf");
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "PDF indirme sırasında bir hata oluştu.");
            }
        }
        [HttpGet]
        public async Task<IActionResult> LessonList()
        {

            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);



            LessonListForStudentByDateRequestModel model = new LessonListForStudentByDateRequestModel
            {
                academicPeriodId = "G",
                AcademicYear = "2023-2024",
                Mail = userInfo.Email

            };
            var val = await _lessonServices.ExamLessonListByStudent(model);

            var academicyears = await _AcademicYearServices.AcademicYearList(userInfo.Email);


            LessonListFirstPageViewModel viewmodel = new LessonListFirstPageViewModel
            {
                ExamModelViews = val,
                academicYearListViewModels = academicyears
            };
            return View(viewmodel);

        }
        public async Task<PartialViewResult> LessonListPartial(AcademicYearListViewModel model)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");

            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);




            LessonListForStudentByDateRequestModel mdl = new LessonListForStudentByDateRequestModel
            {
                academicPeriodId = model.Period,
                AcademicYear = model.YearOfEducation,
                Mail = userInfo.Email

            };
            var val = await _lessonServices.ExamLessonListByStudent(mdl);

            var academicyears = await _AcademicYearServices.AcademicYearList(userInfo.Email);


            LessonListFirstPageViewModel viewmodel = new LessonListFirstPageViewModel
            {
                ExamModelViews = val,
                academicYearListViewModels = academicyears
            };
            return PartialView("_LessonListPartial", viewmodel);
        }

        public async Task<IActionResult> GraduatePage()
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            var val=await  _lessonServices.GetGradInfo(userInfo.Email);
            return View(val.Data);
        }

        public   IActionResult InternPage()
        {
            return View();
        }
    }
}

