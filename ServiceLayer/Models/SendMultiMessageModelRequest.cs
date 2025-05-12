using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class SendMultiMessageModelRequest
    {
        public int LessonId { get; set; }
        public string messagearea { get; set; }
        public string subject { get; set; }
        public string FileName { get; set; }
        public string MessageFileTxt { get; set; }
        public string AcademicianMail { get; set; }

    }
}
