using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.AbstractsModel;
using ServiceLayer.Models;
using ServiceLayer.Requests;
using ServiceLayer.ViewModels;
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
    public class MessageServices:IMessagesServices
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public MessageServices(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }


        public async Task<string> SendMultiMessage(SendMultiMessageModelRequest model,IFormFile file)
        {
            
            try
            {
                if (model.subject == null || model.messagearea == null || model.LessonId == null)
                {
                    return "validation_error";
                }

                if (file != null )
                {
                    var val = ConvertToBase64File(file);
                    model.FileName = file.FileName;
                    model.MessageFileTxt = val;
                }


                var response = await _httpClient.PostAsJsonAsync("Message/SendMultiMessage", model);

                if (response.IsSuccessStatusCode)
                {
                    return "true";
                }
                return "false";

                
            }
            catch (Exception)
            {
                return "HATA";
                throw;
            }


        }

        public async Task<bool> DelDraft(int Id,string email)
        {
            var val = _tokenService.FindTokenByMail(email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);
            var response = await _httpClient.PostAsJsonAsync($"Message/DelDraftMail?Id={Id}", new { });
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }

        public string ConvertToBase64File(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {

                using (var memoryStream = new MemoryStream())
                {
                    file.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();

                    string base64File = Convert.ToBase64String(fileBytes);


                    return  base64File;
                }
            }
            else
            {
                return "false";
            }
        }

        public async Task<string> SendMail(string tomessage, string subject, string messagearea, IFormFile file,string frommessage,string draft,string DraftDelId)
        {
            var valtoken = _tokenService.FindTokenByMail(frommessage);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", valtoken);
            try
            {
                if (tomessage == null || messagearea == null || frommessage == null || subject == null)
                {
                    return "validation_error";
                }
            

            
                SendMailModel request = new SendMailModel()
                {   
                     
                    Content = messagearea,
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    IsDraft = false,
                    IsSended = false,
                    ReceiverMail = tomessage,
                    SenderMail = frommessage,
                    Title = subject
                };

                //if (IsDraft=="true")
                //{
                //    request.IsDraft = false;
                //    request.IsSended = true;
                //}
                if (draft=="true")
                {
                    request.IsDraft = true;
                    request.IsSended = false;
                    
                }
                if (file != null && draft!="true")
                {
                    var val = ConvertToBase64File(file);
                    request.FileName = file.FileName;
                    request.MessageFileTxt = val;
                }
 
                var response = await _httpClient.PostAsJsonAsync("Message/SendMessage", request);

                if (!DraftDelId.IsNullOrEmpty() && DraftDelId!="undefined")
                {
                    var val= DelDraft(int.Parse(DraftDelId),frommessage);
                }

                if (response != null && response.IsSuccessStatusCode && draft == "true")
                {
                    return "draft_true";
                }
                if (response != null && response.IsSuccessStatusCode) 
                {
                    return "true";
                }
               
                return "false";
            }
            catch (Exception)
            {
                return "HATA";
                throw;
            }
        




        }

        public async Task<ICollection<MessageInBoxDto>>InBoxList(string Email)
        {
            var val = _tokenService.FindTokenByMail(Email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"Message/InBoxList?EMail={Email}", new { });
                var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<ICollection<MessageInBoxDto>>>();

                return successResponse.Data;
            }
            catch (Exception)
            {

                throw;
            }
         
           
            
       
        }

        public async Task<ICollection<MessageInBoxDto>> OutBoxList(string Email)
        {
            var val = _tokenService.FindTokenByMail(Email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"Message/OutBoxList?EMail={Email}", new { });
                var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<ICollection<MessageInBoxDto>>>();

                return successResponse.Data;
            }
            catch (Exception)
            {

                throw;
            }




        }

        public async Task<ICollection<MessageInBoxDto>> DraftList(string Email)
        {
            var val = _tokenService.FindTokenByMail(Email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"Message/DraftList?EMail={Email}", new { });
                var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<ICollection<MessageInBoxDto>>>();
              
                return successResponse.Data;
                
            }
            catch (Exception)
            {

                throw;
            }




        }

        public async Task<CustomResponseDto <MessageInBoxDto>> GetMail(int Id, string email)
        {
            var val = _tokenService.FindTokenByMail(email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);

            var response = await _httpClient.PostAsJsonAsync($"Message/GetInBox?Id={Id}", new { });
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<MessageInBoxDto>>();
            return successResponse;

        }

        public async Task<FileDto> DownloadFile(int Id,string email)
        {
            var val = _tokenService.FindTokenByMail(email);
            _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", val);

            var response = await _httpClient.PostAsJsonAsync($"Message/DownloadFile?Id={Id}", new { });
            var successResponse = await response.Content.ReadFromJsonAsync<CustomResponseDto<FileDto>>();

            FileDto fileDto = new FileDto()
            {
                FileName = successResponse.Data.FileName,
                Id = successResponse.Data.Id,
                MessageFileTxt = successResponse.Data.MessageFileTxt
            };
            return fileDto;
        }

        public string GetMimeTypeFromFileName(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return ext switch
            {
                ".pdf" => "application/pdf",
                ".jpg" => "image/jpeg",
                ".png" => "image/png",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream",  
            };
        }

         
    }
}
