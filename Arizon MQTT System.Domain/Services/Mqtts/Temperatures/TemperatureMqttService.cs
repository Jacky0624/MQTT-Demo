using Arizon_MQTT_System.Domain.Models.Settings;
using Arizon_MQTT_System.Domain.Models.Temperatures;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Domain.Services.Mqtts.Temperatures
{
    public class TemperatureMqttService : ITemperatureMqttService
    {
        private readonly MqttFactory _mqttFactory;
        private IMqttClient _mqttClient;
        public event Action<TemperatureItemMqtt> OnReceieve;
        public event Action OnConnectionStateChanged;
        public readonly MqttClientConfiguration _mqttClientConfiguration;
        public bool IsConnected => _mqttClient.IsConnected;

        public TemperatureMqttService(MqttFactory mqttFactory,
            MqttClientConfiguration mqttClientConfiguration)
        {
            _mqttFactory = mqttFactory;
            _mqttClientConfiguration = mqttClientConfiguration;
            Initial();
            StartDetectLoop();
        }
        private void Initial()
        {
            _mqttClient = _mqttFactory.CreateMqttClient();
            _mqttClient.ApplicationMessageReceivedAsync += _client_ApplicationMessageReceivedAsync;
            _mqttClient.ConnectedAsync += _mqttClient_ConnectedAsync;
            _mqttClient.DisconnectedAsync += _mqttClient_DisconnectedAsync;
        }

        private Task _mqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            OnConnectionStateChanged?.Invoke();
            return Task.CompletedTask;
        }

        private Task _mqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            OnConnectionStateChanged?.Invoke();
            return Task.CompletedTask;
        }
        private CancellationTokenSource _cancel = new CancellationTokenSource();
        private void StartDetectLoop()
        {
            _cancel.Cancel();
            _cancel = new CancellationTokenSource();
            Task.Factory.StartNew(async() =>
            {
                while (!_cancel.IsCancellationRequested)
                {
                    try
                    {
                        if (!_mqttClient.IsConnected)
                        {
                            await OpenAsync();
                        }
                    }
                    catch (Exception ex)
                    {

                       
                    }
                  
                    await Task.Delay(2000);
                }
            }, _cancel.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
        private Task _client_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            var str = Encoding.ASCII.GetString(e.ApplicationMessage.Payload);
            var item = JsonSerializer.Deserialize<TemperatureItemMqtt>(str);
            OnReceieve?.Invoke(item);
            return Task.CompletedTask;
        }

        public async Task OpenAsync()
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(_mqttClientConfiguration.ServerIp, 1883)
                .Build();
            var response = await _mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
            if(response.ResultCode == MqttClientConnectResultCode.Success)
            {
              
                var mqttSubscribeOptions = _mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(f => { f.WithTopic(_mqttClientConfiguration.Topic); })
                    .Build();

                var res = await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
            }
        }

        public async Task CloseAsync()
        {
            var mqttClientDisconnectOptions = _mqttFactory.CreateClientDisconnectOptionsBuilder().Build();
            await _mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);
        }
    }
}
