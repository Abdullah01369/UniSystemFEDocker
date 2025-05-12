using ServiceLayer.AbstractsModel;
using ServiceLayer.Models;
using ServiceLayer.Requests;
using SharedLayer.Dtos;
using SharedLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ProjectService : IProjectService
    {

        private readonly  ITokenService _tokenService;
        private readonly HttpClient _httpClient;

        public ProjectService(HttpClient httpClient, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _httpClient = httpClient;

        }

        public async Task<CustomResponseDto<StudentRequest>> AddStudentToProject(StudentSavingProjectViewModel request)
        {
            var val = _tokenService.FindTokenByMail(request.Email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);
            var response = await _httpClient.PostAsJsonAsync("Project/AddStudentToProject", request);

            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<StudentRequest>>();
            return successResponse;

        }
        public async Task<CustomResponseDto<StudentsInfoForAcademicianDto>> GetStudentByStudentNo(string No,string email)
        {
            var val = _tokenService.FindTokenByMail(email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);
            var response = await _httpClient.PostAsJsonAsync($"User/FindUserByStudentNo?StudentNo={No}", new { });
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<StudentsInfoForAcademicianDto>>();
            return successResponse;

        }
        public async Task<string> DocumentCount(int Id,string email)
        {
            var value = _tokenService.FindTokenByMail(email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", value);
            var response = await _httpClient.PostAsJsonAsync($"Project/DocumentCount?Id={Id}", new { });
        
            var val=  await response.Content.ReadFromJsonAsync<CustomResponseDto<FileCountDto>>();
            return val.Data.Count.ToString();

        }
        public async Task<CustomResponseDto<List<StudentListForProjectDetail>>> GetStudentList(int Id,string email)
        {
            var value = _tokenService.FindTokenByMail(email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", value);
            var response = await _httpClient.PostAsJsonAsync($"Project/GetStudentListByProject?Id={Id}", new { });
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<StudentListForProjectDetail>>>();
            return successResponse;

        }
        public async Task<CustomResponseDto<ProjectModel>> GetProjectInfoById(int Id,string email)
        {
            var value = _tokenService.FindTokenByMail(email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", value);

            var response = await _httpClient.PostAsJsonAsync($"Project/GetProjectById?Id={Id}", new { });
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProjectModel>>();
            return successResponse;

        }
        public async Task<CustomResponseDto<List<ProjectModel>>> GetProjectList(string email)
        {
            var value = _tokenService.FindTokenByMail(email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", value);
            var response = await _httpClient.PostAsJsonAsync($"Project/GetProjectListByAcademicianMail?Email={email}", new { });
            
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<ProjectModel>>>();
            return successResponse;

        }
    }
}
