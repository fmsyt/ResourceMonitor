using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Configs
{
    internal class ConfigResourceBase : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("Label")]
        public string Label
        {
            get { return (string)this["Label"]; }
            set { this["Label"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("60")]
        public int Capacity
        {
            get { return Int32.Parse(this["Capacity"].ToString() ?? "60"); }
            set { this["Capacity"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("0%")]
        public string Format
        {
            get { return (string)this["Format"]; }
            set { this["Format"] = value; }
        }
    }
}
