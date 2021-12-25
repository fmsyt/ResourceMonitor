using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models.Resources
{
    internal class Memory : SingletonResource<Memory>
    {
        public override float Current()
        {
            throw new NotImplementedException();
        }
    }
}
