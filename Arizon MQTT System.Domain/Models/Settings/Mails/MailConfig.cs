using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Domain.Models.Settings.Mails
{
    public class MailConfig
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("isBodyHtml")]
        public bool IsBodyHtml { get; set; }
    }
}
