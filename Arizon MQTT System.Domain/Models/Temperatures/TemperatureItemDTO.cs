using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Domain.Models.Temperatures
{
    public class TemperatureItemMqtt
    {
        [JsonPropertyName("sensor_id")]
        public int SensorId { get; set; }
        [JsonPropertyName("MAC")]
        public string Mac { get; set; }
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }
        [JsonPropertyName("isDefect")]
        public bool IsDefect { get; set; }
    }
}
