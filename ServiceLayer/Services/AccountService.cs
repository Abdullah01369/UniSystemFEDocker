
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.AbstractsModel;
using ServiceLayer.DbConnection;
using ServiceLayer.Models;
using ServiceLayer.Requests;
using SharedLayer.Dtos;
using SharedLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
 

namespace ServiceLayer.Services
{
    public class AccountService : IAccountServices
    {
        private readonly ITokenService _tokenService;
        private readonly HttpClient _httpClient;
        private readonly DbContextModel _dbContextModel;
        private readonly ILogger<AccountService> _logger;
        private readonly IConfiguration _configuration;


        public AccountService(HttpClient httpClient, DbContextModel dbContextModel, ITokenService tokenService,ILogger<AccountService> logger,IConfiguration configuration)
        {

            _configuration = configuration;
            _dbContextModel = dbContextModel;
            _httpClient = httpClient;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<CustomResponseDto<TokenDto>> LoginStudent(LoginDto loginDto)
        {
           var val= _configuration.GetSection("BaseUrl");
            if (loginDto != null || loginDto.Role == "S")
            {
                var response = await _httpClient.PostAsJsonAsync("Auth/CreateToken", loginDto);
                var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<TokenDto>>();
                return successResponse;
            }
            return null;

        }
        public async Task<CustomResponseDto<TokenDto>> LoginAcademician(LoginDto loginDto)
        {
            loginDto.Role = "A";
            if (loginDto != null || loginDto.Role == "A")
            {
                var response = await _httpClient.PostAsJsonAsync("Auth/CreateToken", loginDto);
                var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<TokenDto>>();
                return successResponse;
            }
            return null;

        }
        public bool SaveUserInfo(UserInfoModel model)
        {
            //    var val = _tokenService.FindTokenByMail(model.Email);
            //        _httpClient.DefaultRequestHeaders.Authorization =
            //new AuthenticationHeaderValue("Bearer", val);
            try
            {
                UserInfo user = new UserInfo
                {

                    Mail = model.Email,
                    AccessToken = model.AccessToken,
                    AccessTokenExpiration = model.AccessTokenExpiration,
                    RefreshToken = model.RefreshToken,
                    RefreshTokenExpiration = model.RefreshTokenExpiration


                };


                _dbContextModel.UserInfos.Add(user);

                _dbContextModel.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;

                throw;
            }


        }
        public UserInfo FindUser(string mail)
        {

            var val = _dbContextModel.UserInfos.ToList();
            var user = _dbContextModel.UserInfos.Where(x => x.Mail == mail).FirstOrDefault();

            return user;

        }
        public async Task<CustomResponseDto<TakeUserInfoModel>> GetUserInfoProfile(string username, string email)
        {

            var val = _tokenService.FindTokenByMail(email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);
            var response = await _httpClient.PostAsJsonAsync($"User/GetUserByName/{username}", new { });

            var dd = response.Content.ReadAsStringAsync();

            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<TakeUserInfoModel>>();
            return successResponse;



        }
        public async Task<bool> UpdateStudentProfile(updatedInfo model)
        {
            var val = _tokenService.FindTokenByMail(model.email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);

            var response = await _httpClient.PostAsJsonAsync("User/UpdateStudentInfo", model);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool UpdateUserInfo(UserInfoModel model, UserInfo user)
        {
            user.RefreshToken = model.RefreshToken;
            user.AccessToken = model.AccessToken;
            user.AccessTokenExpiration = model.AccessTokenExpiration;
            user.RefreshTokenExpiration = model.RefreshTokenExpiration;

            _dbContextModel.UserInfos.Update(user);
            _dbContextModel.SaveChanges();
            return true;
        }

        

        public async Task<CustomResponseDto<NoData>> ForgetPassword(string email)
        {
            var response = await _httpClient.PostAsJsonAsync($"User/ForgetPassword?email={email}", new {});
 
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<NoData>>();
            return successResponse;
        

        }
 
        public async Task<CustomResponseDto<NoData>> IsConfirmToken(string email,string token)
        {
            string encodedToken = WebUtility.UrlEncode(token);
            var response = await _httpClient.PostAsJsonAsync($"User/ResetPasswordTokenValidate?email={email}&token={encodedToken}", new {});

            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<NoData>>();
            return successResponse;
        }
 
        public async Task<CustomResponseDto<NoData>> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            ResetPasswordDto request = new ResetPasswordDto()
            {
                Email = model.Email,
                NewPassword = model.NewPassword,
                Token = model.Token
            };

            var response = await _httpClient.PostAsJsonAsync($"User/resetpassword ", request);

            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<NoData>>();
    
            return successResponse;
        }

        public async Task<CustomResponseDto<TokenDto>> LoginAdmin(AdminLoginViewModel loginDto)
        {

         
                var response = await _httpClient.PostAsJsonAsync("Auth/CreateToken", loginDto);
                var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<TokenDto>>();
                return successResponse;
           
       

        }






    }

}



