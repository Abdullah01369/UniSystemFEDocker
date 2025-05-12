using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.AbstractsModel;
using SharedLayer.Dtos;
using SharedLayer.ViewModels;

namespace UniSystemFE.Areas.Student.Controllers
{
    [Area("Student")]
    public class ProfileController : Controller
    {

        private readonly IAnnouncementService _announcementService;
        private readonly ILessonServices _lessonServices;
        private readonly IAccountServices _accountServices;
        private readonly IAcademicYearServices _AcademicYearServices;


        public ProfileController(IAnnouncementService announcementService, ILessonServices lessonServices, IAccountServices account, IAcademicYearServices academicYearServices)
        {
            _announcementService = announcementService;
            _lessonServices = lessonServices;
            _accountServices = account;
            _AcademicYearServices = academicYearServices;
        }
        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            
            var val = await _accountServices.GetUserInfoProfile(userInfo.UserName, userInfo.Email);


            return View(val);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile([FromBody] updatedInfo model)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");

            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
           
            model.username = userInfo.UserName;
            model.email = userInfo.Email;
            var result = await _accountServices.UpdateStudentProfile(model);

            // result return true or false
            return Json(result);
        }
    }
}
