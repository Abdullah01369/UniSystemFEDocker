using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.AbstractsModel;
using ServiceLayer.Services;
using SharedLayer.Dtos;
using SharedLayer.ViewModels;

namespace UniSystemFE.Areas.Academician.Controllers
{
    [Area("Academician")]
    public class CourseManagementController : Controller
    {
        private readonly IAcademicianService _AcademicianService;


        public CourseManagementController(IAcademicianService academicianService)
        {

            _AcademicianService = academicianService;
        }

        public async Task<IActionResult> Ask()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ask(string prompt)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (string.IsNullOrEmpty(userInfoJson))
            {
                return Json(new { success = false, message = "Oturum süresi dolmuş. Lütfen tekrar giriş yapın." });
            }

            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);

            try
            {
                var analysisResult = await _AcademicianService.Ask(prompt, userInfo.Email);

                return Json(new
                {
                    success = true,
                    message = "Analiz başarıyla tamamlandı.",
                    data = analysisResult
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Bir hata oluştu. Lütfen tekrar deneyin.",
                    error = ex.Message
                });
            }
        }

        public async Task<IActionResult> StudentListByCourse(int Id)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);

            

            StudentListByLessonForAcademicianRequestModel req = new StudentListByLessonForAcademicianRequestModel
            {
                LessonId = Id,
                 AcademicianMail = userInfo.Email


            };
            var val = await _AcademicianService.StudentInfoByLessonForAcademician(req);

            return PartialView("_StudentListPartial", val);
        }


        public async Task<IActionResult> EnterGrades(int Id)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);

            

            StudentListByLessonForAcademicianRequestModel req = new StudentListByLessonForAcademicianRequestModel
            {
                LessonId = Id,
                AcademicianMail = userInfo.Email


            };

           
            var val = await _AcademicianService.StudentInfoExamsByLessonForAcademician(req);

            if (val.Count()==0)
            {
                ViewBag.LessonName = "Ders İçin Öğrenci Girişi Henüz Yapılmadı...";

            }
            else
            {
                ViewBag.LessonName = val.First().LessonName.ToUpper();

            }


            return View(val);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMidTerm(string ExamId,string NewScore,string StudentNo)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            SavingMidtermGradeModel model = new SavingMidtermGradeModel()
            {
                ExamId = ExamId,
                MidtermScore = NewScore,
                StudentNo = StudentNo,
                 AcademicianMail=userInfo.Email
            };

            if (ModelState.IsValid)
            {
                var val = await _AcademicianService.SaveMidtermGrade(model);
                if (val == true)
                {
                    return Json("true");
                }
                return Json("false");

            }
            return Json("false");

        }


        [HttpPost]
        public async Task<IActionResult> SaveFinal(string ExamId, string NewScore, string StudentNo)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            SaveFinalExamModel model = new SaveFinalExamModel ()
            {
                ExamId = ExamId,
                FinalScore = NewScore,
                StudentNo = StudentNo,
                 AcademicianMail = userInfo.Email
            };

            if (ModelState.IsValid)
            {
                var val = await _AcademicianService.SaveFinal(model);
                if (val == true)
                {
                    return Json("true");
                }
                return Json("false");

            }
            return Json("false");

        }

        [HttpPost]
        public async Task<IActionResult> SaveBut(string ExamId, string NewScore, string StudentNo)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);

            SaveButModel model = new SaveButModel()
            {
                ExamId = ExamId,
                ButScore = NewScore,
                StudentNo = StudentNo,
                 AcademicianMail=userInfo.Email
            };

            if (ModelState.IsValid)
            {
                var val = await _AcademicianService.SaveBut(model);
                if (val == true)
                {
                    return Json("true");
                }
                return Json("false");

            }
            return Json("false");

        }
    }
}
