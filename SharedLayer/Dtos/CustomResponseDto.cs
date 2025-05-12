using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }

      
        public int StatusCode { get; set; }

        [JsonProperty("Error")]

        public  ErrorDto Error { get; set; }


    }
}
