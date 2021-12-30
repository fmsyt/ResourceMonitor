using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models
{
    internal interface IResource
    {
        public float Current();
    }
}
