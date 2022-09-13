using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT__System.Services.Messages
{
    public interface IMessageService
    {
        void ShowErrorMessage(string error, string caption);
        void ShowNormalMessage(string msg, string caption);
        bool ShowYesNoDialog(string msg, string caption);
    }
}
