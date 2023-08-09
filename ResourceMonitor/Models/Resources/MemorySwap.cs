using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models.Resources
{
    internal class MemorySwap : Resource
    {
        protected ManagementClass mc;
        protected ManagementObjectCollection? moc = null;

        public MemorySwap()
        {
            mc = new ManagementClass("Win32_OperatingSystem");
        }

        ~MemorySwap()
        {
            if (moc != null)
            {
                moc.Dispose();
            }
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
