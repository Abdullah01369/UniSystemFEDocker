using ServiceLayer.AbstractsModel;
using ServiceLayer.Models;
using ServiceLayer.Requests;
using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class LessonServices : ILessonServices
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;




        public LessonServices(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;

            _tokenService = tokenService;

        }

        #region metots
        public async Task<string> TakeDepartmentCode(string username, string email)
        {
            var val = _tokenService.FindTokenByMail(email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);
            var response = await _httpClient.PostAsJsonAsync($"User/TakeDepartmentCode?username={username}", new { });
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<TakeDepCode>>();
            return successResponse.Data.Code;

        }
        public async Task<byte[]> DownloadLessonProgramAsync(string code)
        {
            var response = await _httpClient.GetAsync($"Lesson/DownloadLessonProgramByDepartment?code={code}");
            if (!response.IsSuccessStatusCode)
            {
                throw new FileNotFoundException("PDF dosyası bulunamadı.");
            }

            return await response.Content.ReadAsByteArrayAsync();
        }
        public async Task<ICollection<ExamModelView>> ExamLessonListByStudent(LessonListForStudentByDateRequestModel model)
        {
            {
                var val = _tokenService.FindTokenByMail(model.Mail);
                _httpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", val);

                var response = await _httpClient.PostAsJsonAsync("Lesson/LessonListByStudentAndPeriod", model);

                var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<ExamModelView>>>();
                return successResponse.Data;

            }
        }
        public async Task<List<DocumentListDto>> TakeStudentDoc(string email)
        {

            var val = _tokenService.FindTokenByMail(email);


            _httpClient.DefaultRequestHeaders.Authorization =
   new AuthenticationHeaderValue("Bearer", val);

            var response = await _httpClient.GetAsync($"Calculate/DocList/MyDocs/?Email={email}");

            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<DocumentListDto>>>();
            return successResponse.Data;
        }
        public async Task<CustomResponseDto<NoData>> TakeNewRequestStudentDoc(TakeStudentDocModel model)
        {
            var val = _tokenService.FindTokenByMail(model.Email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);


            var response = await _httpClient.PostAsync($"Calculate/DocumentRequest/document-request?StudentNo={model.StudentNo}&Type={model.Type}", null);



            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<NoData>>();

            return successResponse;
        }

        #endregion



        public async Task<Stream> DownloadFile(int Id, string Mail)
        {

            var val = _tokenService.FindTokenByMail(Mail);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);

            var response = await _httpClient.GetAsync($"Calculate/TakeDoc/?Id={Id}");

            var stream = await response.Content.ReadAsStreamAsync();

            return stream;


        }



        public async Task<CustomResponseDto<GradutedInfoDto>> GetGradInfo(string email)
        {
            var val = _tokenService.FindTokenByMail(email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);


            var response = await _httpClient.GetAsync($"User/GetGraduatedInfo?Email={email}");



            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<GradutedInfoDto>>();

            return successResponse;
        }
      
    }
}