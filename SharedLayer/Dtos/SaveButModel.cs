using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{
    public class SaveButModel
    {
        [Required]
        public string StudentNo { get; set; }
        [Required]
        public string ButScore { get; set; }
        [Required]
        public string ExamId { get; set; }

        [Required]
        public string AcademicianMail { get; set; }

    }
}
