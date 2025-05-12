using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{
    public class ResearcherExp
    {
        public int Id { get; set; }
        public int ResearcherId { get; set; }
 
        public string Name { get; set; }
        public string DateBeg { get; set; }
        public string DateEnd { get; set; }
        public string Universty { get; set; }
        public string Duty { get; set; }
    }
}
