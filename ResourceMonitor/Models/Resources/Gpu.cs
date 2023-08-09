using Iot.Device.HardwareMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models.Resources
{
    internal class Gpu : Resource
    {
        public Gpu()
        {
            var monitor = new OpenHardwareMonitor();
        }

        public override float Current()
        {
            return 0;
        }
    }
}
