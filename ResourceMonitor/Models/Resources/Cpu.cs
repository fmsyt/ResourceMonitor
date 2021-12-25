using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models.Resources
{
    internal class Cpu : SingletonResource<Cpu>
    {
        public override int Current()
        {
            throw new NotImplementedException();
        }
    }
}
