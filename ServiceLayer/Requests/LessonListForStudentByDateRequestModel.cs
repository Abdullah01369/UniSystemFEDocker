using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Requests
{
    public class LessonListForStudentByDateRequestModel
    {
        public string Mail { get; set; }
        public string academicPeriodId { get; set; }
        public string AcademicYear { get; set; }


    }
}
