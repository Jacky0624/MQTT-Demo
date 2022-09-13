using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.API.Models
{
    public class MQTTModelingPrepAPIKey
    {
        public MQTTModelingPrepAPIKey(string key)
        {
            Key = key;
        }

        public string Key { get; }


    }
}
