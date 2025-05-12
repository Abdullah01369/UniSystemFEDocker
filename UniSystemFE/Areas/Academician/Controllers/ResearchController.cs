using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using ServiceLayer.AbstractsModel;
using ServiceLayer.Models;
using SharedLayer.Dtos;
using SharedLayer.ViewModels;

namespace UniSystemFE.Areas.Academician.Controllers
{
    [Area("Academician")]
    public class ResearchController : Controller
    {
        private readonly IProjectService _projectService;

        public ResearchController(IProjectService projectService)
        {
            _projectService = projectService;
            
        }
        public async Task <IActionResult> Index()
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            

            var val=await _projectService.GetProjectList(userInfo.Email);
            if (val != null && val.Error==null)
            {
                return View(val.Data);
            }
            ViewBag.Errors = val.Error.Errors;
            return View();
           
        }

        [HttpPost]
        public async Task<JsonResult> AddStudentInfo( [FromBody]StudentSavingProjectViewModel model)
        {

            var userInfoJson = HttpContext.Session.GetString("UserInfo");// "nullsa login yaptırıt"
            
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);

            var val =await   _projectService.GetStudentByStudentNo(model.StudentNo,userInfo.Email);
            if (val.Data != null)
            {
                return Json(val);
            }
            return Json("FAIL");
        }
        [HttpPost]
        public async Task<JsonResult> AddStudent([FromBody] StudentSavingProjectViewModel model)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");// "nullsa login yaptırıt"
        
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            model.Email = userInfo.Email;
            var val = await _projectService.AddStudentToProject(model);
            if (val!= null)
            {
                return Json(val);
            }
            return Json("FAIL");
        }
        

        public async Task<IActionResult> ProjectDetail(int id)
        {
            try
            {
                var userInfoJson = HttpContext.Session.GetString("UserInfo");// "nullsa login yaptırıt"
                if (userInfoJson == null)
                {

                    return RedirectToAction("Index", "Home");

                }
                var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
                string email=userInfo.Email;

                var projectInfo = await _projectService.GetProjectInfoById(id, email);
                var StudentList = await _projectService.GetStudentList(id, email);
               
                var filecount = await _projectService.DocumentCount(id, email);
                ViewBag.documentcount = filecount;

                ProjectDetailModel model = new ProjectDetailModel
                {
                    ProjectModel = projectInfo.Data,
                    StudentCount = StudentList.Data.Count.ToString(),
                    StudentList = StudentList.Data,
                    FileModels = null,
                    ComplateRate = "21"

                };
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
            
           
        }

        public async Task<IActionResult> VideoConference()
        {
            return View();
        }
    }
}
