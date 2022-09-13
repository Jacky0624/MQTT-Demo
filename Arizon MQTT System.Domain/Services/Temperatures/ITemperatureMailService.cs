using Arizon_MQTT_System.Domain.Models.Mails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Domain.Services.Temperatures
{
    public interface ITemperatureMailService
    {
        Task<IEnumerable<TemperatureMailGroup>> FindAllGroupsAsync();
    }
}
