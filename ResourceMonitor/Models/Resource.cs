using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models
{
    internal abstract class Resource
    {
        public int Count { get; set; } = 0;
        public abstract float Current();
    }
}
