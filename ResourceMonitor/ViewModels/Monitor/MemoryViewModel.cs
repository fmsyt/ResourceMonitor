using ResourceMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ResourceMonitor.ViewModels.Monitor
{
    internal class MemoryViewModel : MonitorViewModel<Models.Resources.Memory>
    {
        public MemoryViewModel()
        {
            Initialization("メモリ");
        }
    }
}
