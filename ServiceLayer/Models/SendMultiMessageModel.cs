using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class SendMultiMessageModel
    {
        public int LessonId { get; set; }
        public string messagearea { get; set; }
        public string subject { get; set; }
        public IFormFile file { get; set; }  
    }
}
