using ServiceLayer.AbstractsModel;
using ServiceLayer.Models;
using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.AdminServices
{
    public class CourseServices : ICourseServices
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public CourseServices(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;

        }
        public async Task<CustomResponseDto<PaggingModel>> GetCourseList(string mail, string? page="1",string? pagesize="10", string? search="")
        {
            var token = _tokenService.FindTokenByMail(mail);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

           // var response = await _httpClient.GetAsync("Course/GetAllCourse");
            var response = await _httpClient.GetAsync($"Course/GetAllCourse?page={page}&pagesize={pagesize}&search{search}");


           // var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<CourseDto>>>();
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<PaggingModel>>();
            return successResponse;
            
        }
        public async Task<CustomResponseDto<PaggingModel>> GetCourseList(string mail)
        {
            var token = _tokenService.FindTokenByMail(mail);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // var response = await _httpClient.GetAsync("Course/GetAllCourse");
            var response = await _httpClient.GetAsync($"Course/GetAllCourse");


            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<PaggingModel>>();
            return successResponse;

        }
        public async Task<CustomResponseDto<PaggingModel>> SearchCourse(string mail,string input)
        {
            var token = _tokenService.FindTokenByMail(mail);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // var response = await _httpClient.GetAsync("Course/GetAllCourse");
            var response = await _httpClient.GetAsync($"Course/SearchCourse?input={input}");


            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<PaggingModel>>();
            return successResponse;

        }
        public async Task<CustomResponseDto<List<DepartmentListDto>>> GetAllDepartment(string email)
        {
            var token = _tokenService.FindTokenByMail(email);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

           
            var response = await _httpClient.GetAsync($"Course/DepartmentList");


            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<DepartmentListDto>>>();
            return successResponse;
        }
        public async Task<CustomResponseDto<CourseDto>> AddCourse(string mail,AddCourseDto courseDto)
        {
            var token = _tokenService.FindTokenByMail(mail);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // var response = await _httpClient.GetAsync("Course/GetAllCourse");
            var response = await _httpClient.PostAsJsonAsync($"Course/AddCourse",courseDto);


            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<CourseDto>>();
            return successResponse;
        }
        public async Task<CustomResponseDto<EditCourseDto>> EditCourse(string mail, EditCourseDto courseDto)
        {
            var token = _tokenService.FindTokenByMail(mail);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

    
            var response = await _httpClient.PostAsJsonAsync($"Course/EditCourse", courseDto);


            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<EditCourseDto>>();
            return successResponse;
        }
        public async Task<CustomResponseDto<CourseDto>> GetCourse(string mail, int Id)
        {
            var token = _tokenService.FindTokenByMail(mail);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"Course/GetCourse?Id={Id}");
 
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<CourseDto>>();
            return successResponse;
        }
        public async Task<CustomResponseDto<CourseStatisticModel>> GetStatistic(string mail)
        {
            var token = _tokenService.FindTokenByMail(mail);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"Course/GetCourseStatistic");

            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<CourseStatisticModel>>();
            return successResponse;
        }
    }
}
