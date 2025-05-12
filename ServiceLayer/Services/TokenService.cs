using ServiceLayer.AbstractsModel;
using ServiceLayer.DbConnection;
using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class TokenService:ITokenService
    {
        private readonly DbContextModel _dbContextModel;
        public TokenService(DbContextModel dbContextModel)
        {
            _dbContextModel = dbContextModel;
            
        }
        public DeCodingTokenModel DecodeToken(string token)
        {

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                var claims = jsonToken.Claims;
                var name = claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
                var email = claims.FirstOrDefault(claim => claim.Type == "email")?.Value;

                return new DeCodingTokenModel
                {
                    Mail = email,
                    Name = name

                };
                
            }
            return null;
           

        }

        public string FindTokenByMail(string mail)
        {
           var val= _dbContextModel.UserInfos.Where(x => x.Mail == mail).FirstOrDefault();
            return val.AccessToken;
        }
       

    }
}
