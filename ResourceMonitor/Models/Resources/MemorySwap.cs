using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models.Resources
{
    [SettingsGroupName("MemorySwap")]
    internal class MemorySwap : Resource
    {
        private readonly ManagementClass mc;
        private ManagementObjectCollection? moc = null;

        public MemorySwap()
        {
            mc = new ManagementClass("Win32_OperatingSystem");
        }

        ~MemorySwap()
        {
            moc?.Dispose();
        }

        public override float Current()
        {
            moc = mc.GetInstances();

            float result = 0;
            foreach (ManagementObject mo in moc)
            {
                var free = float.Parse(mo["FreeSpaceInPagingFiles"].ToString() ?? "0");
                var total = float.Parse(mo["SizeStoredInPagingFiles"].ToString() ?? "0");

                result = 1 - free / total;
                mo.Dispose();
            }

            return result;
        }
    }
}
