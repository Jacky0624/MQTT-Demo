using Arizon_MQTT_System.Domain.Models.Mails;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT__System.ViewModels.Mails
{
    public class TemperatureMailGroupViewModel : ViewModelBase
    {
        private readonly TemperatureMailGroup _group;
        public double Upper => _group.Upper;
        public double Lower => _group.Lower;
        public double Interval => _group.Interval;

        private ObservableCollection<TemperatureSendedInfoViewModel> _sendedInfos;
        public IEnumerable<TemperatureSendedInfoViewModel> SendedInfos
        {
            get => _sendedInfos;
        }

        public bool CheckWantSendMail(int sensorId, double interval)
        {
            var item = _sendedInfos.Where(x => x.SensorId == sensorId).FirstOrDefault();
            if(item == null)
            {
                _sendedInfos.Add(new TemperatureSendedInfoViewModel() { LatestSendTime = DateTime.Now, SensorId = sensorId, IsSended = true });
                return true;
            }
            else
            {
                if (DateTime.Now.Subtract(item.LatestSendTime).TotalMinutes > interval || !item.IsSended)
                {
                    item.LatestSendTime = DateTime.Now;
                    item.IsSended = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
           
        }

        public IEnumerable<TemperatureMailItem> MailItems => _group.MailItems;
     
        public TemperatureMailGroupViewModel(TemperatureMailGroup group)
        {
            _group = group;
            _sendedInfos = new ObservableCollection<TemperatureSendedInfoViewModel>();
        }
    }
}
