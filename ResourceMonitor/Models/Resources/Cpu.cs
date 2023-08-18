using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models.Resources
{
    /// <summary>
    /// CPUパフォーマンスモニタでCPUリソース監視する際の主なカウンターについて
    /// <see href="https://rainbow-engine.com/perfmon-processer-important-counters"/>
    /// </summary>
    [SettingsGroupName("Cpu")]
    internal class Cpu : Resource
    {
        private readonly PerformanceCounter cpuCounter;

        public Cpu()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public override float Current()
        {
            return cpuCounter.NextValue();
        }
    }
}
