using Arizon_MQTT_System.Domain.Models.Requests;
using Arizon_MQTT_System.Domain.Models.Responses;
using Arizon_MQTT_System.Domain.Services.Mails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.API.Services
{
    public class MailService : IMailService
    {
        private readonly MESHttpClient _client;

        public MailService(MESHttpClient client)
        {
            _client = client;
        }

        public async Task<ResponseBase> SendAsync(SendMailRequest request)
        {
            string uri = "http://10.35.2.22:8786/Mail/SendMail";
            return await _client.PostAsync<ResponseBase>(uri, request);
        }
    }
}
