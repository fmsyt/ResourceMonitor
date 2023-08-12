using Iot.Device.HardwareMonitor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models.Resources
{
    internal class Gpu : Resource
    {
        protected ProcessStartInfo StartInfo { get; set; }

        public Gpu()
        {
            Label = "GPU";
            //var monitor = new OpenHardwareMonitor();

            StartInfo = new ProcessStartInfo
            {
                FileName = @"nvidia-smi",
                Arguments = "--query-gpu=utilization.gpu --format=csv,noheader,nounits",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
        }

        public override float Current()
        {
            Process p = Process.Start(StartInfo);
            string gpu_usage = p.StandardOutput.ReadToEnd().TrimEnd();

            return float.Parse(gpu_usage) / 100;
        }
    }
}
