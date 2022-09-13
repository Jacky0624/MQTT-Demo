using Arizon_MQTT_System.Domain.Models.Requests;
using Arizon_MQTT_System.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Domain.Services.Mails
{
    public interface IMailService
    {
        Task<ResponseBase> SendAsync(SendMailRequest request);
    }
}
