using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResourceMonitor.Configs;

namespace ResourceMonitor.Models
{
    internal abstract class Resource : ConfigResourceBase
    {
        public int Count { get; set; } = 0;
        public abstract float Current();
    }
}
