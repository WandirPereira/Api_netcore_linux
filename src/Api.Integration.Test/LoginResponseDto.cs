using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Api.Domain.Dtos
{
    public class LoginResponseDto
    {
        [JsonProperty("authenticated")]
        public bool authenticated { get; set; }

        [JsonProperty("create")]
        public DateTime create { get; set; }

        [JsonProperty("expiration")]
        public DateTime expiration { get; set; }

        [JsonProperty("accessToken")]
        public string? accessToken { get; set; }

        [JsonProperty("userName")]
        public string? userName { get; set; }

        [JsonProperty("name")]
        public string? name { get; set; }

        [JsonProperty("message")]
        public string? message { get; set; }
    }
}
