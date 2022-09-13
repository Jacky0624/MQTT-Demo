using Arizon_MQTT__System.Commands.Mails;
using Arizon_MQTT__System.Models;
using Arizon_MQTT__System.Services.Messages;
using Arizon_MQTT__System.Stores;
using Arizon_MQTT_System.Domain.Models.Mails;
using Arizon_MQTT_System.Domain.Models.Requests;
using Arizon_MQTT_System.Domain.Models.Settings.Mails;
using Arizon_MQTT_System.Domain.Services.Mails;
using Arizon_MQTT_System.Domain.Services.Temperatures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Arizon_MQTT__System.ViewModels.Mails
{
    public class SendTemperatureMailViewModel : ViewModelBase
    {
        private ObservableCollection<TemperatureMailGroupViewModel> _mailGroups;
        public IEnumerable<TemperatureMailGroupViewModel> MailGroups => _mailGroups;
        private readonly SendTemperatureMailStore _sendMailStore;
        private List<TemperatureItem> _buffer => _sendMailStore.Items;
        private readonly IMailService _mailService;
        private readonly MailConfig _mailConfig;
      
        public ICommand RefreshCommad { get; }
        public SendTemperatureMailViewModel(
            IMailService mailService, 
            MailConfig mailConfig,
            SendTemperatureMailStore sendMailStore,
            ITemperatureMailService temperatureMailService,
            IMessageService messageService)
        {
            
            _mailService = mailService;
            _mailConfig = mailConfig;
            _sendMailStore = sendMailStore;
            RefreshCommad = new RefreshSendTemperatureMailViewModelConfigsCommand(this, temperatureMailService, messageService);
            _mailGroups = new ObservableCollection<TemperatureMailGroupViewModel>();
            var list = new List<TemperatureMailItem>();
            list.Add(new TemperatureMailItem() { Email = "xiao.yu@yeontech.com" });
            list.Add(new TemperatureMailItem() { Email = "yentai.liu@yeontech.com" });
            RefreshCommad.Execute(null);
            var group = new TemperatureMailGroup()
            {
                Interval = 5,
                Upper = 60,
                Lower = 0,
                MailItems = list
            };
            _mailGroups.Add(
                new TemperatureMailGroupViewModel(group));
            StartLoop();
        }

        public void Add(TemperatureItem item)
        {
            _buffer.Add(item);
        }
        public void UpdateConfigs(IEnumerable<TemperatureMailGroup> groups)
        {
            _mailGroups.Clear();
            foreach (var group in groups)
            {
                _mailGroups.Add(new TemperatureMailGroupViewModel(group));
            }
        }
        private CancellationTokenSource _cancel = new CancellationTokenSource();
        private void StartLoop()
        {
            _cancel.Cancel();
            _cancel = new CancellationTokenSource();
            Task.Factory.StartNew(async () =>
            {
                while(!_cancel.IsCancellationRequested)
                {
                    if (_buffer.Count > 0)
                    {
                        try
                        {
                            var item = _buffer[0];
                            _buffer.RemoveAt(0);
                            var needSendGroup = 
                            _mailGroups.Where(
                                x => //x.CheckWantSendMail(item.SensorId, x.Interval) &&
                                x.Upper >= item.Temperature &&
                                x.Lower <= item.Temperature);
                            foreach (var group in needSendGroup)
                            {
                                if(group.CheckWantSendMail(item.SensorId, group.Interval))
                                {
                                    var sendRequest = new SendMailRequest()
                                    {
                                        To = group.MailItems.Select(x => x.Email),
                                        Body = $"{item.SensorId} 於{item.Time.ToString("yyyy/MM/dd HH:mm:ss")} 溫度高達 {item.Temperature} 度",
                                        Subject = $"sensor id : {item.SensorId} 溫度警報",
                                        Cc = new List<string>(),
                                        Config = _mailConfig
                                    };
                                    var result = await _mailService.SendAsync(sendRequest);
                                    if (result.ResultCode == 0)
                                    {

                                    }
                                }
                               
                           
                            }
                           
                        }
                        catch (Exception ex)
                        {

                        }
                     
                    }
                    await Task.Delay(1000);
                }
             
            }, _cancel.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }


    }
}
