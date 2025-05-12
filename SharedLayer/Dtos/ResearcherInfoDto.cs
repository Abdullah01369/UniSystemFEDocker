using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{
    public class ResearcherInfoDto
    {
        public int Id { get; set; }
        public string InstitutionalInformation { get; set; }
        public string WoSResearchAreas { get; set; }
        public string SpecialResearchAreas { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
    }
}
