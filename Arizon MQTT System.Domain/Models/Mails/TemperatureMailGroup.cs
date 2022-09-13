using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Domain.Models.Mails
{
    public class TemperatureMailGroup
    {
        [JsonPropertyName("upper")]
        public double Upper { get; set; }
        [JsonPropertyName("lower")]
        public double Lower { get; set; }
        [JsonPropertyName("interval")]
        public double Interval { get; set; }
        [JsonPropertyName("mailItems")]
        public IEnumerable<TemperatureMailItem> MailItems { get; set; }
    }
}
