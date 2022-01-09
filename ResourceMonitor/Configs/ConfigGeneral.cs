using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Configs
{
    internal class ConfigGeneral : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("False")]
        public bool Topmost
        {
            get { return System.Convert.ToBoolean(this["Topmost"]); }
            set { this["Topmost"] = value; }
        }
    }
}
