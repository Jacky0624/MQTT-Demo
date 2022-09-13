using Arizon_MQTT_System.Domain.Models.Mails;
using Arizon_MQTT_System.Domain.Services.Temperatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.API.Services
{
    public class TemperatureMailService : ITemperatureMailService
    {
        private readonly MESHttpClient _client;

        public TemperatureMailService(MESHttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<TemperatureMailGroup>> FindAllGroupsAsync()
        {
            return await _client.GetAsync<IEnumerable<TemperatureMailGroup>>("http://10.35.2.22:9530/TemperatureMail/GetMailConfig");
        }
    }
}
