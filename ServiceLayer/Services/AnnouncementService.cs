using Newtonsoft.Json.Linq;
using ServiceLayer.AbstractsModel;
using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public AnnouncementService(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;

            _tokenService = tokenService;

        }
        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<List<AnnouncementModelDto>>> GetAll(string mail)

        {
            var token = _tokenService.FindTokenByMail(mail);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("Announcement");

            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<AnnouncementModelDto>>>();
            return successResponse;

        }

        public Task<bool> Save(AnnouncementModelDto modelDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
