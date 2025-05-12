using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.ViewModels
{
    public class AcademicianLessonsDto
    {
        public string AppUserId { get; set; }
        public string AppName { get; set; }

        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public string LessonCode { get; set; }

        public int AcademicYearId { get; set; }
        public string AcademicYearPeriod { get; set; }
        public string AcademicYear { get; set; }

        public bool IsActive { get; set; }
    }
}
