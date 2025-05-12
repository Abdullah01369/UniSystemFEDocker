using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{
    public class SendMailModel
    {
         
        public string ReceiverMail { get; set; }
        public string SenderMail { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public string? FileName { get; set; }

        public bool IsDeleted { get; set; }
        public string? MessageFileTxt { get; set; }
        public bool IsSended { get; set; }
        public bool IsDraft { get; set; }
 
     }
}
