using Arizon_MQTT_System.Domain.Models.Temperatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Domain.Services.Mqtts.Temperatures
{
    public interface ITemperatureMqttService : IMqttService<TemperatureItemMqtt>
    {
    }
}
