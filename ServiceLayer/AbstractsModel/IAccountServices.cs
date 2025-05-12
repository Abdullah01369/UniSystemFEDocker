using ServiceLayer.Models;
using ServiceLayer.Requests;
using SharedLayer.Dtos;
using SharedLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AbstractsModel
{
    public interface IAccountServices
    {
        Task<CustomResponseDto<TokenDto>> LoginStudent(LoginDto loginDto);
        Task<CustomResponseDto<TokenDto>> LoginAcademician(LoginDto loginDto);
        Task<CustomResponseDto<TokenDto>> LoginAdmin(AdminLoginViewModel loginDto);


        bool SaveUserInfo(UserInfoModel model);
        bool UpdateUserInfo(UserInfoModel model, UserInfo user);
        public UserInfo FindUser(string mail);
        Task<CustomResponseDto<TakeUserInfoModel>> GetUserInfoProfile(string username, string email);
        Task<bool> UpdateStudentProfile(updatedInfo model);
        Task<CustomResponseDto<NoData>> ForgetPassword(string email);
        Task<CustomResponseDto<NoData>> IsConfirmToken(string email, string token);
        Task<CustomResponseDto<NoData>> ResetPasswordAsync(ResetPasswordViewModel model);





    }
}
