using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceLayer.AbstractsModel;
using ServiceLayer.Models;
using ServiceLayer.Services.AdminServices;
using SharedLayer.Dtos;
using System.Drawing.Printing;

namespace UniSystemFE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagementController : Controller
    {
        private readonly ICourseServices _courseServices;


        public ManagementController(ICourseServices courseServices)
        {
            _courseServices = courseServices;

        }
        public IActionResult Index()
        {
            return View();
        }
 
        public async Task<IActionResult> LessonManagement()
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return RedirectToAction("Index", "Home");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            var statistic = await _courseServices.GetStatistic(userInfo.Email);
            ViewBag.CourseSum = statistic.Data.Sum;
            ViewBag.CourseActive = statistic.Data.Active;
            ViewBag.CoursePassive = statistic.Data.Passive;


            var val = await _courseServices.GetCourseList(userInfo.Email);
            if (val.Data != null)
            {
                val.Data.CurrentPage = 1;
                val.Data.TotalPages = (int)Math.Ceiling((double)val.Data.Page / 10);
                return View(val.Data);
            }

            ViewBag.Control = "Beklenmedik bir hata aldık";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> PaggingTable(int pageNumber, int pageSize = 10)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return Redirect("/Admin/Home/Login");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            var courses = await _courseServices.GetCourseList(userInfo.Email, pageNumber.ToString(), pageSize.ToString(), "");




            var model = new PaggingModel
            {
                CourseList = courses.Data.CourseList,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling((double)courses.Data.Page / pageSize)
            };

            return PartialView("_CourseTablePartial", model);
        }
        [HttpGet]
        public async Task<IActionResult> SearchLesson(string input)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return Redirect("/Admin/Home/Login");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            var courses = await _courseServices.SearchCourse(userInfo.Email, input);


            if (courses.Data == null)
            {

                return PartialView("_CourseTablePartial", null);
            }

            var model = new PaggingModel
            {
                CourseList = courses.Data.CourseList,
                CurrentPage = 0,
                TotalPages = (int)Math.Ceiling((double)courses.Data.Page / 10)
            };

            return PartialView("_CourseTablePartial", model);
        }
        [HttpGet]
        public async Task<IActionResult> AddCourse()
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return Redirect("/Admin/Home/Login");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);

            var val = await _courseServices.GetAllDepartment(userInfo.Email);
            if (val.Data!=null)
            {
                ViewBag.Departments = val.Data;  
                return View();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse(AddCourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return Redirect("/Admin/Home/Login");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);
            var result = await _courseServices.AddCourse(userInfo.Email,courseDto);
            TempData["SuccessMessage"] = "Ders Başarıyla Eklendi";

            return View(new AddCourseDto { });
        }

        [HttpGet]
        public async Task<IActionResult> EditCourse(int Id)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return Redirect("/Admin/Home/Login");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);

            var courseInfo =  await _courseServices.GetCourse(userInfo.Email,Id);
            var val = await _courseServices.GetAllDepartment(userInfo.Email);
            if (val.Data != null  && courseInfo.Data!=null)
            {
                ViewBag.SelectedDepartmentId = courseInfo.Data.DepartmentId; // Seçili departman ID
                ViewBag.SelectedStatus = courseInfo.Data.Status; // Seçili durum (true: Aktif, false: Pasif)

                ViewBag.Departments = val.Data;
               
                return View(courseInfo.Data);
                  
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditCourse(CourseDto courseDto)
        {
            EditCourseDto dto = new EditCourseDto
            {
                 Code=courseDto.Code,
                  Credit=courseDto.Credit,
                   Name=courseDto.Name,
                    Id= courseDto.Id,
                     DepartmentId = courseDto.DepartmentId
                     

                      

            };
            if (!ModelState.IsValid)
            {
                return View();
            }
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (userInfoJson == null)
            {

                return Redirect("/Admin/Home/Login");

            }
            var userInfo = JsonConvert.DeserializeObject<UserInfoModel>(userInfoJson);

            EditCourseDto dtoedit = new EditCourseDto()
            {
                Code = courseDto.Code,
                Credit = courseDto.Credit,
                DepartmentId = courseDto.DepartmentId,
                Name = courseDto.Name,
                Status = courseDto.Status,
                Id = courseDto.Id
            };
            var result = await _courseServices.EditCourse(userInfo.Email,dtoedit);

            if (result.StatusCode!=201)
            {
                TempData["FailMessage"] = "Başaramadın, Neyi? dersi güncellemeyi";

                return View(new CourseDto { });
            }
            TempData["SuccessMessage"] = "Ders Başarıyla Güncellendi";

            return View(new CourseDto { });
        }




    }
}



