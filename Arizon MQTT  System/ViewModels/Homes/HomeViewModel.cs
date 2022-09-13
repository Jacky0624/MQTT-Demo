using Arizon_MQTT__System.Commands.Mqtts;
using Arizon_MQTT__System.Models;
using Arizon_MQTT__System.Services.Messages;
using Arizon_MQTT__System.Stores;
using Arizon_MQTT__System.ViewModels.Temperatures;
using Arizon_MQTT_System.Domain.Models.Temperatures;
using Arizon_MQTT_System.Domain.Services.Mqtts;
using Arizon_MQTT_System.Domain.Services.Mqtts.Temperatures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace Arizon_MQTT__System.ViewModels.Homes
{
    public class HomeViewModel : ViewModelBase
    {
        private ObservableCollection<TemperatureItemViewModel> _items;
        public IEnumerable<TemperatureItemViewModel> Items => _items;

        private readonly ITemperatureMqttService _mqttService;
        
        public bool IsConnected
        {
            get => _mqttService.IsConnected;
        }
        private readonly SendTemperatureMailStore _sendMailStore;
        public ICommand OpenCommand { get; }
        public ICommand CloseCommand { get; }
        public HomeViewModel(
            ITemperatureMqttService mqttService,
            IMessageService messageService,
            SendTemperatureMailStore sendMailStore)
        {
            _sendMailStore = sendMailStore;
            _items = new ObservableCollection<TemperatureItemViewModel>();
            OpenCommand = new OpenMqttCommand<TemperatureItemMqtt>(mqttService, messageService);
            CloseCommand = new CloseMqttCommand<TemperatureItemMqtt>(mqttService, messageService);
            _mqttService = mqttService;
            _mqttService.OnReceieve += _mqttService_OnReceieve;
            _mqttService.OnConnectionStateChanged += _mqttService_OnConnectionStateChanged;
        }

        private void _mqttService_OnConnectionStateChanged()
        {
            OnPropertyChanged(nameof(IsConnected));
        }

        private object _lock = new object();

        private void _mqttService_OnReceieve(TemperatureItemMqtt obj)
        {
            lock (_lock)
            {
                var item = _items.Where(x => x.SensorId == obj.SensorId).FirstOrDefault();
                var o = new TemperatureItem()
                {
                    SensorId = obj.SensorId,
                    IsDefect = obj.IsDefect,
                    Mac = obj.Mac,
                    Temperature = obj.Temperature,
                    Time = DateTime.Now
                };
               
                _sendMailStore.Add(o);
                
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if (item == null)
                    {
                       
                        _items.Add(new TemperatureItemViewModel(o));
                    }
                    else
                    {
                        item.IsDefect = obj.IsDefect;
                        item.Temperature = obj.Temperature;
                        item.Time = DateTime.Now;
                       
                    }
                }));
               
            }
          
        }
    }
}
