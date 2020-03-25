using System;
using Newtonsoft.Json;

namespace Edu.Ntu.Foundation.Core.ErrorResponses
{
    public class Error : ErrorCode
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "$id")]
        public String id { get; set; }
        public string errorCode { get; set; }
        public String errorMessage
        {
            get { return message; }
            set { message = value; }
        }
    }
}