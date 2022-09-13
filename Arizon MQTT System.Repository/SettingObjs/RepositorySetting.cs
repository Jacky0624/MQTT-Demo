using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizon_MQTT_System.Repository.SettingObjs
{
    public class RepositorySetting : IRepositorySetting
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
    }
}
