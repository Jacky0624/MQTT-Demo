using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Repository.SettingObjs
{
    public interface IRepositorySetting
    {
        string UserID { get; set; }
        string Password { get; set; }
        string DataSource { get; set; }
        string InitialCatalog { get; set; }
    }
}
