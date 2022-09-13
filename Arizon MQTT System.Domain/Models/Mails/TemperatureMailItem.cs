using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Domain.Models.Mails
{
    public class TemperatureMailItem : IMailItem
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("isTurnOn")]
        public bool IsTurnOn { get; set; }
    
    }
}
