using SharedLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class ProjectDetailModel
    {
        public ProjectModel ProjectModel { get; set; }
        public string StudentCount { get; set; }
        public string ComplateRate { get; set; }
        public List<StudentListForProjectDetail> StudentList { get; set; }
        public List<FileModel> FileModels { get; set; }
    }
}
