using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{
    public class TakeUserInfoModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? No { get; set; }
        public string? TC { get; set; } 
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; } 
        public string? Phone { get; set; } 
        public string? PhotoBase64Text { get; set; }

        public int? AddressId { get; set; }
        public string? AddressDec { get; set; }
    }
}
