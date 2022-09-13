using Arizon_MQTT__System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT__System.Models
{
    public class TemperatureItem 
    {
        public int SensorId { get; set; }
        public string Mac { get; set; }
        public double Temperature { get; set; }
        public bool IsDefect { get; set; }
        public DateTime Time { get; set; }
    }
}
