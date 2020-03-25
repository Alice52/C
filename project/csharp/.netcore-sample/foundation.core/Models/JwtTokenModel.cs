using System;
using Newtonsoft.Json;

namespace Edu.Ntu.Foundation.Core.Models
{
    public class JwtTokenModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiressIn { get; set; }
    }
}
