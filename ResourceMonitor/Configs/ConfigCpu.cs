using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Configs
{
    internal class ConfigCpu : ConfigResourceBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("CPU")]
        public new string Label
        {
            get { return base.Label; }
            set { base.Label = value; }
        }

    }
}
