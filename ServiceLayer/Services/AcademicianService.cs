
using ServiceLayer.AbstractsModel;
using ServiceLayer.Requests;
using SharedLayer.Dtos;
using SharedLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    // bearer eklendi
    public class AcademicianService : IAcademicianService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public AcademicianService(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;

        }

        public async Task<ICollection<AcademicianLessonsDto>> AcademicianLessonList(string UserName, string Mail)
        {
            var val = _tokenService.FindTokenByMail(Mail);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer",val);
            var response = await _httpClient.PostAsJsonAsync($"Academician/ListAcademicianLessons?UserName={UserName}", new { });
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<ICollection<AcademicianLessonsDto>>>();
            return successResponse.Data;

        }
        public async Task<IEnumerable<StudentsInfoForAcademicianDto>> StudentInfoByLessonForAcademician(StudentListByLessonForAcademicianRequestModel request)
        {
            var val = _tokenService.FindTokenByMail(request.AcademicianMail);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);
            var response = await _httpClient.PostAsJsonAsync("Academician/StudentListByCourseForAcademician", request);
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<IEnumerable<StudentsInfoForAcademicianDto>>>();
            if (successResponse != null && successResponse.Data != null)
            {
                return successResponse.Data;
            }

            return Enumerable.Empty<StudentsInfoForAcademicianDto>();


        }
        public async Task<IEnumerable<StudentExamListResponseModel>> StudentInfoExamsByLessonForAcademician(StudentListByLessonForAcademicianRequestModel request)
        {
            var val = _tokenService.FindTokenByMail(request.AcademicianMail);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);
            var response = await _httpClient.PostAsJsonAsync("Academician/StudentExamListByCourseForAcademician", request);
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<IEnumerable<StudentExamListResponseModel>>>();
            if (successResponse != null && successResponse.Data != null)
            {
                return successResponse.Data;
            }

            return Enumerable.Empty<StudentExamListResponseModel>();


        }
        public async Task<bool> SaveMidtermGrade(SavingMidtermGradeModel request)
        {
            var val = _tokenService.FindTokenByMail(request.AcademicianMail);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);
            var response = await _httpClient.PostAsJsonAsync("Academician/SaveMidtermGrade", request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> SaveFinal(SaveFinalExamModel request)
        {
            var val = _tokenService.FindTokenByMail(request.AcademicianMail);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);
            var response = await _httpClient.PostAsJsonAsync("Academician/SaveFinal", request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> SaveBut(SaveButModel request)
        {
            var val = _tokenService.FindTokenByMail(request.AcademicianMail);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);
            var response = await _httpClient.PostAsJsonAsync("Academician/SaveBut", request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }


        public async Task<string> Ask(string prompt,string Mail)
        {
            var val = _tokenService.FindTokenByMail(Mail);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);
            var response = await _httpClient.PostAsJsonAsync($"AiInformation/askgroq?prompt={prompt}", new { });
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<string>>();
            return successResponse.Data;

        }


    }
}
