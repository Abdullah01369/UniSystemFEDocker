using ServiceLayer.AbstractsModel;
using ServiceLayer.Requests;
using ServiceLayer.ViewModels;
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
    public class AcademicYearServices:IAcademicYearServices
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public AcademicYearServices(HttpClient httpClient, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _httpClient = httpClient;

           

        }

        public async Task<ICollection<AcademicYearListViewModel>> AcademicYearList(string email)
        {
            {
                var val = _tokenService.FindTokenByMail(email);
                _httpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", val);
                var response = await _httpClient.GetAsync("Lesson/ListAcademicYear");
                var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<AcademicYearListViewModel>>>();
                return successResponse.Data;

            }
        }
    }
}
