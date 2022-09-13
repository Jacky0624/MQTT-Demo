using Arizon_MQTT__System.Models;
using Arizon_MQTT_System.Domain.Models.Mails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT__System.Stores
{
    public class SendTemperatureMailStore 
    {
        private List<TemperatureItem> _items;
        public List<TemperatureItem> Items => _items;

        public SendTemperatureMailStore()
        {
            _items = new List<TemperatureItem>();
        }

        public void Add(TemperatureItem item)
        {
            _items.Add(item);
        }
    }
}
