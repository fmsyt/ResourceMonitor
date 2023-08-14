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
        protected ProcessStartInfo StartInfo;

        public Gpu()
        {
            Label = "GPU";

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
            try
            {
                var p = Process.Start(StartInfo);
                if (p == null)
                {
                    return 0;
                }

                string gpu_usage = p.StandardOutput.ReadToEnd().TrimEnd();

                return float.Parse(gpu_usage) / 100;
            } catch
            {
                return 0;
            }
        }
    }
}
