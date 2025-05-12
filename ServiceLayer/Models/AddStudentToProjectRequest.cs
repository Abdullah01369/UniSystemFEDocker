using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class AddStudentToProjectRequest
    {

        [Required]
        public string StudentNo { get; set; }
        [Required]
        public string ProjectId { get; set; }
    }
}
