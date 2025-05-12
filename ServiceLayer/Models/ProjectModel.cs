using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string AcademicianNameSurname { get; set; }
        public string AcademicianMail { get; set; }
        public string Declaration { get; set; }
        public string CreadtedDate { get; set; }
        public string Name { get; set; }
        public bool IsPublish { get; set; }
    }
}
