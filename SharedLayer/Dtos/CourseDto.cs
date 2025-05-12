using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Credit { get; set; }
        public bool Status { get; set; }
        public string? DepartmentName { get; set; }
        public int DepartmentId { get; set; }
    }
}
