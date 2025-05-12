using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{
    public class StudentListByLessonForAcademicianRequestModel
    {
        public string AcademicianMail { get; set; }
        public int LessonId { get; set; }

    }
}
