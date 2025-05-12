using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using ServiceLayer.AbstractsModel;
using ServiceLayer.Models;
using SharedLayer.Dtos;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace UniSystemFE.Areas.Academician.Controllers
{
    [Area("Academician")]
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
                 AcademicianMail=userInfo.Email,
                LessonId = model.LessonId,
                messagearea = model.messagearea,
                subject = model.subject
            };
            var val=await _messagesService.SendMultiMessage(request, model.file);
            
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
                var val = await _messagesService.GetMail(id.Value,userInfo.Email);

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
            var val = await _messagesService.DownloadFile(Id,userInfo.Email);


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
            var val = await _messagesService.GetMail(Id,userInfo.Email);
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
            var val = await _messagesService.DelDraft(id,userInfo.Email);
            if (val == true)
            {
                return Json(true);
            }
            return Json(false);

        }


    }
}



#region
//[HttpGet]
//public IActionResult SendMailredirect()
//{
//    var a = 123;

//    return View();
//}

//public async Task<IActionResult> UploadToDrive(IFormFile file)
//{
//    if (file == null || file.Length == 0)
//    {
//        return BadRequest("Dosya bulunamadı.");
//    }

//    var clientId = _config["GoogleDrive:ClientId"];
//    var clientSecret = _config["GoogleDrive:ClientSecret"];
//    var scopes = new[] { _config["GoogleDrive:Scopes:0"] };
//    var applicationName = _config["GoogleDrive:ApplicationName"];
//    var redirectUri = "http://localhost:5000/signin-google";

//    UserCredential credential;
//    using (var stream = new MemoryStream())
//    {

//        credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
//            new ClientSecrets
//            {
//                ClientId = clientId,
//                ClientSecret = clientSecret
//            },
//            scopes,
//            "user",


//            CancellationToken.None,
//            new FileDataStore("token.json", true));
//    }

//    var service = new DriveService(new BaseClientService.Initializer()
//    {
//        HttpClientInitializer = credential,
//        ApplicationName = applicationName,

//    });

//    var filePath = Path.GetTempFileName();
//    using (var stream = new FileStream(filePath, FileMode.Create))
//    {
//        await file.CopyToAsync(stream);
//    }

//    var driveFile = new Google.Apis.Drive.v3.Data.File()
//    {
//        Name = file.FileName
//    };

//    using (var fileStream = new FileStream(filePath, FileMode.Open))
//    {
//        var request = service.Files.Create(driveFile, fileStream, file.ContentType);
//        request.Fields = "id";
//        var uploadProgress = await request.UploadAsync();

//        if (uploadProgress.Status == Google.Apis.Upload.UploadStatus.Completed)
//        {
//            return Ok(new { FileId = request.ResponseBody.Id });
//        }
//        else
//        {
//            return StatusCode(500, "Dosya yüklenemedi.");
//        }
//    }
//}
#endregion


