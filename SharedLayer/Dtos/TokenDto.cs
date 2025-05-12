using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{
    public class TokenDto
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("accessTokenExpiration")]
        public DateTime AccessTokenExpiration { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("refreshTokenExpiration")]
        public DateTime RefreshTokenExpiration { get; set; }

    }


}
