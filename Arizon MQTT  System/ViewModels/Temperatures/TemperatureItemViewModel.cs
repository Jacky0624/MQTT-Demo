using Arizon_MQTT__System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT__System.ViewModels.Temperatures
{
    public class TemperatureItemViewModel : ViewModelBase
    {
      

        public TemperatureItemViewModel(TemperatureItem item)
        {
            SensorId = item.SensorId;
            Mac = item.Mac;
            Temperature = item.Temperature;
            IsDefect = item.IsDefect;
            Time = item.Time;
        }

        private int _sensorId;
        public int SensorId
        {
            get => _sensorId;
            set
            {
                if(_sensorId != value)
                {
                    _sensorId = value;
                    OnPropertyChanged("SensorId");
                }
            }
        }
        private string _mac;
        public string Mac
        {
            get => _mac;
            set
            {
                if(_mac != value)
                {
                    _mac = value;
                    OnPropertyChanged("Mac");
                }
            }
        }
        private double _temperature;
        public double Temperature
        {
            get => _temperature;
            set
            {
                if(_temperature != value)
                {
                    _temperature = value;
                    OnPropertyChanged("Temperature");
                }
            }
        }
        private bool _isDefect;
        public bool IsDefect
        {
            get => _isDefect;
            set
            {
                if(_isDefect != value)
                {
                    _isDefect = value;
                    OnPropertyChanged("IsDefect");
                }
            }
        }
        private DateTime _time;
        public DateTime Time
        {
            get => _time;
            set
            {
                if(_time != value)
                {
                    _time = value;
                    OnPropertyChanged("Time");
                }
            }
        }
    }
}
