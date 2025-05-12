
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.AbstractsModel;
using ServiceLayer.Models;
using ServiceLayer.Services;
using SharedLayer.Dtos;
using System.Runtime.CompilerServices;

namespace UniSystemFE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IAccountServices _accountServices;
        private readonly ITokenService _tokenService;
        public HomeController(IAccountServices services, ITokenService tokenService)
        {
            _accountServices = services;
            _tokenService = tokenService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _accountServices.LoginAdmin(model);

            if (response.StatusCode == 200)
            {
                var value = _tokenService.DecodeToken(response.Data.AccessToken);
                UserInfoModel userInfo = new UserInfoModel
                {
                    Email = value.Mail,
                    AccessToken = response.Data.AccessToken,
                    AccessTokenExpiration = response.Data.AccessTokenExpiration,
                    RefreshToken = response.Data.RefreshToken,
                    RefreshTokenExpiration = response.Data.RefreshTokenExpiration,
                    UserName = value.Name,


                };
                var user = _accountServices.FindUser(model.Email);
                if (user is null)
                {

                    _accountServices.SaveUserInfo(userInfo);
                }
                else
                {
                    _accountServices.UpdateUserInfo(userInfo, user);
                }
                HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(userInfo));

                return Redirect("/Admin/Management/Index");

            }


            response.Error.Errors.ForEach(x =>
            {
                ModelState.AddModelError(string.Empty, x);
                
            });
            return View();
        }
       


    }
}
