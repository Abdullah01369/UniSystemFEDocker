using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels
{
    public class LessonListFirstPageViewModel
    {
        public ICollection<ExamModelView>  ExamModelViews { get; set; }
        public ICollection<AcademicYearListViewModel>  academicYearListViewModels { get; set; }
    }
}
