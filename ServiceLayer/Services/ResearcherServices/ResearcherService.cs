using ServiceLayer.AbstractsModel;
using ServiceLayer.Models;
using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.ResearcherServices
{
    public class ResearcherService : IResearcherService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;




        public ResearcherService(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;

            _tokenService = tokenService;

        }


        public async Task<CustomResponseDto<ResearcherInfoDto>> ResearcherInfo(string email)
        {
             
            var response = await _httpClient.GetAsync($"Researcher/GetInformationForResarcher?Email={email}");
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<ResearcherInfoDto>>();
            return successResponse;

        }

        public async Task<CustomResponseDto<ResearchMetricDto>> GetMetrics(string email)
        {
          
            var response = await _httpClient.GetAsync($"Researcher/GetInformationResarcherMetrics?Email={email}");
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<ResearchMetricDto>>();
            return successResponse;

        }

        public async Task<CustomResponseDto<EducationInfoDto>> GetEducInfo(string email)
        {

            var response = await _httpClient.GetAsync($"Researcher/GetInformationEdu?Email={email}");
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<EducationInfoDto>>();
            return successResponse;

        }
        public async Task<CustomResponseDto<List<ResearchAreaDto>>> GetResearcherAreaInfo(string email)
        {

            var response = await _httpClient.GetAsync($"Researcher/GetResearhAreas?Email={email}");
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<ResearchAreaDto>>>();
            return successResponse;

        }

        public async Task<CustomResponseDto<List<ResearcherExp>>> GetResearcherExp(string email)
        {

            var response = await _httpClient.GetAsync($"Researcher/GetExperience?Email={email}");
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<ResearcherExp>>>();
            return successResponse;

        }

        public async Task<CustomResponseDto<List<ResearcherPublication>>> GetPublication(string email)
        {

            var response = await _httpClient.GetAsync($"Researcher/GetPublications?Email={email}");
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<ResearcherPublication>>>();
            return successResponse;

        }
        public async Task<CustomResponseDto<ContactResearcherDto>> GetContact(string email)
        {

            var response = await _httpClient.GetAsync($"Researcher/GetContactInfo?Email={email}");
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<ContactResearcherDto>>();
            return successResponse;

        }
    }
}