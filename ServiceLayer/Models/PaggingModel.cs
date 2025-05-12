using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class PaggingModel
    {
        public List<CourseDto> CourseList { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
