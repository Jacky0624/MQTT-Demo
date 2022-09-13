using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Domain.Models.Responses
{
    public class ResponseBase
    {
        [JsonPropertyName("resultCode")]
        public int ResultCode { get; set; }
        [JsonPropertyName("resultMessage")]
        public string ResultMessage { get; set; }
    }
}
