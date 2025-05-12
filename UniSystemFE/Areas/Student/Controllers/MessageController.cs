using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.AbstractsModel;
using ServiceLayer.Models;
using SharedLayer.Dtos;

namespace UniSystemFE.Areas.Student.Controllers
{
    [Area("Student")]
    public class MessageController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IMessagesServices _messagesService;


        public MessageController(IConfiguration config, IMessagesServices messagesService)
        {
            _config = config;
            _messagesService = messagesService;
        }


        [HttpGet]
        public async Task<IActionResult> SendMultiMessageToStudents(int id)
        {
            TempData["LessonId"] = id;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SendMultiMessage(SendMultiMessageModel model)
        {
            if (TempData["LessonId"] != null)
            {
                model.LessonId = (int)TempData["LessonId"];
            }
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);


            SendMultiMessageModelRequest request = new SendMultiMessageModelRequest()
            {
                AcademicianMail = userInfo.Email,
                LessonId = model.LessonId,
                messagearea = model.messagearea,
                subject = model.subject
            };
            var val = await _messagesService.SendMultiMessage(request, model.file);

            return Json(val);
        }



        [HttpGet]
        public async Task<IActionResult> SendMail(int? id)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            if (id != null)
            {
                var val = await _messagesService.GetMail(id.Value, userInfo.Email);

                return View(val);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(string tomessage, string subject, string messagearea, IFormFile file, string draft, string DraftDelId)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");// "nullsa login yaptırıt"
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);

            var val = await _messagesService.SendMail(tomessage, subject, messagearea, file, userInfo.Email, draft, DraftDelId);

            return Json(val);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFile(int Id)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");// "nullsa login yaptırıt"
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            var val = await _messagesService.DownloadFile(Id, userInfo.Email);


            byte[] fileBytes = Convert.FromBase64String(val.MessageFileTxt);

            string fileName = val.FileName;
            string contentType = _messagesService.GetMimeTypeFromFileName(fileName);


            return File(fileBytes, contentType, fileName);
        }

        [HttpGet]
        public async Task<IActionResult> OutBox()
        {

            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);

            var val = await _messagesService.OutBoxList(userInfo.Email);
            return View(val);
        }

        [HttpGet]
        public async Task<IActionResult> InBox()
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);

            var val = await _messagesService.InBoxList(userInfo.Email);
            return View(val);

        }

        [HttpGet]
        public async Task<IActionResult> ReadMail(int Id)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");// "nullsa login yaptırıt"
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            var val = await _messagesService.GetMail(Id, userInfo.Email);
            return View(val);

        }

        [HttpGet]
        public async Task<IActionResult> DraftList()
        {

            var userInfoJson = HttpContext.Session.GetString("UserInfo");// "nullsa login yaptırıt"
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);

            var val = await _messagesService.DraftList(userInfo.Email);
            return View(val);

        }

        public async Task<JsonResult> DelDraft(int id)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");// "nullsa login yaptırıt"

            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            var val = await _messagesService.DelDraft(id, userInfo.Email);
            if (val == true)
            {
                return Json(true);
            }
            return Json(false);

        }


    }
}
