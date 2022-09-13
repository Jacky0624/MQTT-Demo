using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Domain.Models.Mails
{
    public interface IMailItem
    {
        string Email { get; set; }
        bool IsTurnOn { get; set; }
    }
}
