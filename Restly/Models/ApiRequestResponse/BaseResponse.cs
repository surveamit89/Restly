using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restly.Models.ApiRequestResponse
{
    public class BaseResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("statusCode")]
        public long StatusCode { get; set; }
    }
}
