using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Arizon_MQTT__System.Services.Messages
{
    public class MessageBoxService : IMessageService
    {
        public void ShowErrorMessage(string error, string caption)
        {
            MessageBox.Show(error, caption, MessageBoxButton.OK, MessageBoxImage.Error);

        }

        public void ShowNormalMessage(string msg, string caption)
        {
            MessageBox.Show(msg, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool ShowYesNoDialog(string msg, string caption)
        {
            return MessageBox.Show(msg, caption, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
        }
    }
}
