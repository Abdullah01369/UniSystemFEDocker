using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.AbstractsModel;
using ServiceLayer.Services;
using SharedLayer.Dtos;
using System.Net.Http.Headers;
using System.Net.Http;
using System.IO;

namespace UniSystemFE.Areas.Student.Controllers
{
    [Area("Student")]
    public class DocumentController : Controller
    {

        private readonly ILessonServices _lessonServices;
        private readonly IAccountServices _accountServices;



        public DocumentController(ILessonServices lessonServices, IAccountServices accountServices)
        {
            _accountServices = accountServices;
            _lessonServices = lessonServices;
        }


        public IActionResult Index()
        {
            return View();
        }


        //LİSTELEME

        public async Task<IActionResult> DocumentRequest()
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            var val = await _lessonServices.TakeStudentDoc(userInfo.Email);

            //   var successResponse = await val..ReadFromJsonAsync<CustomResponseDto<List<AnnouncementModelDto>>>();


            return View(val);
        }



        //YENİ BELGE İSTEK
        [HttpPost]
        public async Task<IActionResult> TakeDoc(string DocNo)
        {
            try
            {
                var userInfoJson = HttpContext.Session.GetString("UserInfo");
                if (userInfoJson == null)
                {

                    return RedirectToAction("Index", "Home");

                }


                var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
                var user = await _accountServices.GetUserInfoProfile(userInfo.UserName, userInfo.Email);
                TakeStudentDocModel model = new TakeStudentDocModel()
                {
                    Type = DocNo,
                    Email = userInfo.Email,
                    StudentNo = user.Data.No

                };
                var response = await _lessonServices.TakeNewRequestStudentDoc(model);
                if (response.StatusCode == 200)
                {
                    return Json("1");
                }

                if (response.StatusCode == 404)
                {
                    return Json("2");
                }
                return Json("0");

            }
            catch (Exception)
            {
                return Json("0");
                throw;
            }

        }

        // OLANI İNDİRME
        [HttpGet]
        public async Task<IActionResult> DownloadFile(int Id)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            var val = await _lessonServices.DownloadFile(Id, userInfo.Email);
            return File(val, "application/pdf", "document.pdf");
        }

    



    }
}
