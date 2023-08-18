using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResourceMonitor.Configs;

namespace ResourceMonitor.Models
{
    public delegate float CurrentHandler();

    internal class Resource : ConfigResourceBase
    {
        public CurrentHandler? CurrentHandler = null;

        public Resource() { 
            if (this.Label == "")
            {
                this.Label = this.GetType().Name;
            }
        }

        public int Count { get; set; } = 0;

        public virtual float Current()
        {
            return CurrentHandler != null ? CurrentHandler.Invoke() : 0;
        }

        public void SaveLabel(string label)
        {
            Label = label;
            Save();
        }
    }
}
