using Azure;
using Microsoft.AspNetCore.Http;
using ServiceLayer.Models;
using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AbstractsModel
{
    public interface IMessagesServices
    {
        public Task<string> SendMail(string tomessage, string subject, string messagearea, IFormFile file, string frommessage, string draft, string DraftDelId);
        public string ConvertToBase64File(IFormFile file);
        public Task<ICollection<MessageInBoxDto>> InBoxList(string Email);
        public Task<CustomResponseDto<MessageInBoxDto>> GetMail(int Id, string email);
        public Task<FileDto> DownloadFile(int Id, string email);
        public string GetMimeTypeFromFileName(string fileName);
        public Task<ICollection<MessageInBoxDto>> DraftList(string Email);
        public Task<bool> DelDraft(int Id, string email);
        public Task<ICollection<MessageInBoxDto>> OutBoxList(string Email);
        public Task<string> SendMultiMessage(SendMultiMessageModelRequest model, IFormFile file);







    }
}
