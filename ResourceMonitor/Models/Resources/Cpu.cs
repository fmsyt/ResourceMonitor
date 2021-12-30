using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models.Resources
{
    internal class Cpu : SingletonResource<Cpu>, IResource
    {
        private PerformanceCounter[] cpuCounters;

        public Cpu()
        {
            cpuCounters = new PerformanceCounter[Environment.ProcessorCount];

            for (int i = 0; i < cpuCounters.Length; i++)
            {
                cpuCounters[i] = new PerformanceCounter("Processor", "% Processor Time", i.ToString());
            }
        }
        
        public override float Current()
        {
            var result = GetUsage().Average();
            return result;
        }

        public float[] GetUsage()
        {
            float[] vs = new float[cpuCounters.Length];
            for (int i = 0; i < cpuCounters.Length; i++)
            {
                vs[i] = cpuCounters[i].NextValue();
            }

            return vs;
        }
    }
}
