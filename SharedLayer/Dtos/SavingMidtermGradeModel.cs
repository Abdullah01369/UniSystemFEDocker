using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{
    public class SavingMidtermGradeModel
    {

        [Required]
        public string StudentNo { get; set; }
        [Required]
        public string MidtermScore { get; set; }
        [Required]
        public string ExamId { get; set; }

        [Required]
        public string AcademicianMail { get; set; }

    }
}
