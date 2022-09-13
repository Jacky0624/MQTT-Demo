using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Domain.Models.Settings
{
    public class MqttClientConfiguration
    {
        public string Topic { get; set; }
        public string ServerIp { get; set; }
        public int ServerPort { get; set; }
    }
}
