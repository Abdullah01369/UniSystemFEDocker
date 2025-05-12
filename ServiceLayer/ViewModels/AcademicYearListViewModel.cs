using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels
{
    public class AcademicYearListViewModel
    {
        public int Id { get; set; }
        public string YearOfEducation { get; set; }
        public string Period { get; set; } // Y, G, B
    }
}
