using ResourceMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ResourceMonitor.ViewModels.Monitor
{
    internal class CpuViewModel : MonitorViewModel<Models.Resources.Cpu>
    {
        public CpuViewModel()
        {
            Initialization();
        }
    }
}
