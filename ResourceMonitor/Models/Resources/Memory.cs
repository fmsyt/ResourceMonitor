﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models.Resources
{
    [SettingsGroupName("Memory")]
    internal class Memory : Resource
    {
        private readonly ManagementClass mc;
        private ManagementObjectCollection? moc = null;

        public Memory()
        {
            mc = new ManagementClass("Win32_OperatingSystem");
        }

        ~Memory()
        {
            moc?.Dispose();
        }

        public override float Current()
        {
            moc = mc.GetInstances();

            float result = 0;
            foreach (ManagementObject mo in moc)
            {
                var freePhysicalMemory = float.Parse(mo["FreePhysicalMemory"].ToString() ?? "0");
                var totalVisibleMemorySize = float.Parse(mo["TotalVisibleMemorySize"].ToString() ?? "0");

                result = 1 - freePhysicalMemory / totalVisibleMemorySize;
                mo.Dispose();
            }

            return result;
        }
    }
}
