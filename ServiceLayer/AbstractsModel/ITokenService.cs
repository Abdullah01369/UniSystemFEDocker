using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AbstractsModel
{
    public interface ITokenService
    {
        public DeCodingTokenModel DecodeToken(string token);
        public string FindTokenByMail(string mail);
        

    }
}
