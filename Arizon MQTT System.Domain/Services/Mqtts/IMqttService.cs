using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Domain.Services.Mqtts
{
    public interface IMqttService<T>
    {
        event Action<T> OnReceieve;
        bool IsConnected { get; }
        event Action OnConnectionStateChanged;
        Task OpenAsync();
        Task CloseAsync();

    }
}
