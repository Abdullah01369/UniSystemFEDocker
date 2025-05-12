using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{
    public class ResearcherPublication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ResearcherId { get; set; }
        public string PublishedArea { get; set; }
        public ICollection<PublicationMemberDto> PublicationMembers { get; set; }
    }
}
