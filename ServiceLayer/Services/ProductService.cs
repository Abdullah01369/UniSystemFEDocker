using Azure.Core;
using Newtonsoft.Json.Linq;
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
    public class ProductService: IProductService
    {
        private readonly ITokenService _tokenService;
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _httpClient = httpClient;

        }

        public async Task<CustomResponseDto<List<ProductViewModel>>> ProductList(string email)
        {
            var val = _tokenService.FindTokenByMail(email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);
            var response = await _httpClient.GetAsync("Product/ListOfProduct");

            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<ProductViewModel>>>();
            return successResponse;

        }

        public async Task<string> Buy(PaymentRequest request)
        {
            var val = _tokenService.FindTokenByMail(request.UserMail);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);
            var response = await _httpClient.PostAsJsonAsync("Payment/Pay",request);
 
            var successResponse = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(successResponse);

             
            string htmlContent = jsonObject["content"].ToString();

            return htmlContent;

        }
    }
}
