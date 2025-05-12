using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{
    public class EducationInfoDto
    {
        public int Id { get; set; }
        public string Doctorate { get; set; }
        public string DoctorateDate { get; set; }
        public string Postgraduate { get; set; }
        public string PostgraduateDate { get; set; }
        public string Undergraduate { get; set; }
        public string UndergraduateDate { get; set; }

        public string ForegnLang { get; set; }
        public string ForegnLangII { get; set; }
    }
}
