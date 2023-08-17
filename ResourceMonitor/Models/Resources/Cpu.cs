using System;
using System.Collections.Generic;
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
    internal class Cpu : Resource
    {
        private PerformanceCounter cpuCounter;

        public Cpu()
        {
            Label = "CPU";
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public override float Current()
        {
            return cpuCounter.NextValue() / 100;
        }
    }
}
