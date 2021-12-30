﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models.Resources
{
    internal class Memory : SingletonResource<Memory>, IResource
    {

        protected ManagementClass mc;
        protected ManagementObjectCollection? moc = null;

        public Memory()
        {
            mc = new ManagementClass("Win32_OperatingSystem");
        }

        ~Memory()
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

                var freePhysicalMemory = float.Parse(mo["FreePhysicalMemory"].ToString());
                var totalVisibleMemorySize = float.Parse(mo["TotalVisibleMemorySize"].ToString());

                result = 1 - freePhysicalMemory / totalVisibleMemorySize;
                mo.Dispose();
            }

            return result;
        }
    }
}