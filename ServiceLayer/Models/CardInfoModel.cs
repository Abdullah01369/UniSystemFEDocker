using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class CardInfoModel
    {
        public string cvc { get; set; }
     
        public string name { get; set; }
        public string surname { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string cardno { get; set; }
      
       
    }
}
