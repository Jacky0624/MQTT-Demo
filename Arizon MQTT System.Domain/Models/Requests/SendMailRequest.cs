using Arizon_MQTT_System.Domain.Models.Settings.Mails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Domain.Models.Requests
{
    public class SendMailRequest
    {
        [JsonPropertyName("config")]
        public MailConfig Config { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }
        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        [JsonPropertyName("to")]
        public IEnumerable<string> To { get; set; }
        [JsonPropertyName("cc")]
        public IEnumerable<string> Cc { get; set; }
    }
}
