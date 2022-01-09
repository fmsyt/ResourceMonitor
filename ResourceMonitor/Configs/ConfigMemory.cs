using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Configs
{
    internal class ConfigMemory : ConfigResourceBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("Mem")]
        public new string Label
        {
            get { return base.Label; }
            set { base.Label = value; }
        }
    }
}
