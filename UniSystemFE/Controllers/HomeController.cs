using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using ServiceLayer.AbstractsModel;
using ServiceLayer.Models;
using ServiceLayer.Services;
using SharedLayer.Dtos;
using System.IdentityModel.Tokens.Jwt;
using UniSystemFE.Models;

namespace UniSystemFE.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountServices _accountService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<HomeController> _logger;
        private readonly IAnnouncementService _announcementService;

        public HomeController(IAccountServices accountService, ITokenService tokenService, ILogger<HomeController> logger, IAnnouncementService announcementService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _logger = logger;
            _announcementService = announcementService;
        }
        public async  Task<IActionResult> Index()
        {
            
            return View();
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(String.Empty, "Boş Bırakmayın");

                return View();
            }

            var response = await _accountService.ForgetPassword(request.Email);

            if (response.StatusCode == 404)
            {
                ModelState.AddModelError(String.Empty, "Bu email adresine sahip kullanıcı bulunamamıştır.");
                return View();
            }


            if (response != null && response.StatusCode == 200)
            {
                TempData["SuccessMessage"] = "Şifre yenileme linki, eposta adresinize gönderilmiştir";

            }
            else
            {
                TempData["SuccessMessage"] = "Bir problemle karşılaştık, daha sonra tekrar deneyiniz";
            }

            return RedirectToAction(nameof(ForgetPassword));
        }
        [HttpPost]
        public async Task<IActionResult> LoginStudent([FromBody] LoginDto loginDto)
        {

            loginDto.Role = "S";
            var resp = await _accountService.LoginStudent(loginDto);

            if (resp.Data != null && resp.Data.AccessToken != null)
            {
                var val = _tokenService.DecodeToken(resp.Data.AccessToken);
                UserInfoModel userInfo = new UserInfoModel
                {
                    Email = val.Mail,
                    AccessToken = resp.Data.AccessToken,
                    AccessTokenExpiration = resp.Data.AccessTokenExpiration,
                    RefreshToken = resp.Data.RefreshToken,
                    RefreshTokenExpiration = resp.Data.RefreshTokenExpiration,
                    UserName = val.Name,


                };
                 var user = _accountService.FindUser(loginDto.email);
                 if (user is null)
                {
                     _accountService.SaveUserInfo(userInfo);
                    _logger.LogInformation("RECORDED USER INFO");

                }
                else
                {

                    _accountService.UpdateUserInfo(userInfo, user);
                    _logger.LogInformation("UPDATED USER INFO");

                }


                HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(userInfo));
                return Json(new { success = true, token = resp.Data.AccessToken });
            }
            List<string> err = new List<string>();
            resp.Error.Errors.ForEach(x =>
            {
                err.Add(x);

            });
            return Json(new { success = false, errors = err });
        }
        [HttpPost]
        public async Task<IActionResult> LoginAcademician([FromBody] LoginDto loginDto)
        {
            var resp = await _accountService.LoginAcademician(loginDto);

            if (resp.Data != null && resp.Data.AccessToken != null)
            {
                var val = _tokenService.DecodeToken(resp.Data.AccessToken);
                UserInfoModel userInfo = new UserInfoModel
                {
                    Email = val.Mail,
                    AccessToken = resp.Data.AccessToken,
                    AccessTokenExpiration = resp.Data.AccessTokenExpiration,
                    RefreshToken = resp.Data.RefreshToken,
                    RefreshTokenExpiration = resp.Data.RefreshTokenExpiration,
                    UserName = val.Name,
                };
                var user = _accountService.FindUser(loginDto.email);
                if (user is null)
                {

                    _accountService.SaveUserInfo(userInfo);
                }
                else
                {
                    _accountService.UpdateUserInfo(userInfo, user);
                }


                HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(userInfo));
                return Json(new { success = true, token = resp.Data.AccessToken });
            }
            List<string> err = new List<string>();
            resp.Error.Errors.ForEach(x =>
            {
                err.Add(x);

            });
            return Json(new { success = false, errors = err });
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ResetPassword(string email, string token)
        {

            var result = await _accountService.IsConfirmToken(email, token.ToString());

            if (result.StatusCode == 404)
            {
                ViewBag.Error = result.Error.Errors[0].ToString();

                TempData["ErrorData"] = result.Error.Errors[0].ToString();
                return View(new ResetPasswordViewModel { });

            }


            return View(new ResetPasswordViewModel { Email = email, Token = token });
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.Email == null)
                {
                    ModelState.AddModelError(string.Empty, "Tekrardan Mail ile doğrulama almanız gerekiyor, sayfadan çıkabilirsiniz");
                    return View(new ResetPasswordViewModel { Email = model.Email, Token = model.Token });
                }
                ModelState.AddModelError(string.Empty, "Uyarıları dikkate alın");

                return View(new ResetPasswordViewModel { Email = model.Email, Token = model.Token });
            }
            var val = await _accountService.ResetPasswordAsync(model);

            if (val.Error == null && val.StatusCode == 201)
            {
                TempData["SuccessData"] = "Şifreniz başarıyla değiştirildi...";
                return View(new ResetPasswordViewModel { Email = model.Email, Token = model.Token });

            }

            foreach (var item in val.Error.Errors)
            {
                ModelState.AddModelError(string.Empty, item);

            }

            return View(new ResetPasswordViewModel { Email = model.Email, Token = model.Token });
        }
        public IActionResult PasswordResetSuccess()
        {
            return View();
        }

    }


}// gelen kullanıcıyı kaydet  jwtyi çöz 
 // tokenını kaydet 


// S STUDENT
// A ACADEMICIAN
// AD ADMİN