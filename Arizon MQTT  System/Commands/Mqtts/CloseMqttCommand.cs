﻿using Arizon_MQTT__System.Services.Messages;
using Arizon_MQTT_System.Domain.Services.Mqtts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT__System.Commands.Mqtts
{
    public class CloseMqttCommand<T> : AsyncCommandBase
    {
        private readonly IMqttService<T> _mqttService;
        private readonly IMessageService _messageService;

        public CloseMqttCommand(
            IMqttService<T> mqttService,
            IMessageService messageService)
        {
            _mqttService = mqttService;
            _messageService = messageService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _mqttService.CloseAsync();
            }
            catch (Exception ex)
            {
                _messageService.ShowErrorMessage(ex.Message, "");
            }
        }
    }
}
