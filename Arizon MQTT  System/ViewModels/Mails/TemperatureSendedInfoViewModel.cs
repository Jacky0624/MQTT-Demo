using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT__System.ViewModels.Mails
{
    public class TemperatureSendedInfoViewModel : ViewModelBase
    {
        public int SensorId { get; set; }

    
        private bool _isSended;
        public bool IsSended
        {
            get => _isSended;
            set
            {
                if (_isSended != value)
                {
                    _isSended = value;
                    OnPropertyChanged("IsSended");
                }
            }
        }
        private DateTime _latestSendTime;
        public DateTime LatestSendTime
        {
            get => _latestSendTime;
            set
            {
                if (_latestSendTime != value)
                {
                    _latestSendTime = value;
                    OnPropertyChanged(nameof(LatestSendTime));
                }
            }
        }
    }
}
