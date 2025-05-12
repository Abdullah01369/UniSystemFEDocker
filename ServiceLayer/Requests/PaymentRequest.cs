using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Requests
{
    public class PaymentRequest
    {
        public string ProductId { get; set; }
        public string UserMail { get; set; }
    }
}
