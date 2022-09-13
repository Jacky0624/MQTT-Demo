using Arizon_MQTT__System.Services.Messages;
using Arizon_MQTT__System.ViewModels.Mails;
using Arizon_MQTT_System.Domain.Services.Temperatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT__System.Commands.Mails
{
    public class RefreshSendTemperatureMailViewModelConfigsCommand : AsyncCommandBase
    {
        private readonly SendTemperatureMailViewModel _sendTemperatureMailViewModel;
        private readonly ITemperatureMailService _temperatureMailService;
        private readonly IMessageService _messageService;
        public RefreshSendTemperatureMailViewModelConfigsCommand(
            SendTemperatureMailViewModel sendTemperatureMailViewModel,
            ITemperatureMailService temperatureMailService,
            IMessageService messageService)
        {
            _sendTemperatureMailViewModel = sendTemperatureMailViewModel;
            _temperatureMailService = temperatureMailService;
            _messageService = messageService;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                var list = new List<TemperatureMailGroupViewModel>();
                var items = await _temperatureMailService.FindAllGroupsAsync();
                _sendTemperatureMailViewModel.UpdateConfigs(items);
               
            }
            catch (Exception ex)
            {
                _messageService.ShowErrorMessage(ex.Message, "");
                
            }
        }
    }
}
