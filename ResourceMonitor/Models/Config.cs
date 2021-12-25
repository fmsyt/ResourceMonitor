using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models
{
    internal class Config
    {
        protected static int capacity;
        public static int Capacity { get { return capacity; } }

        public Config()
        {
            Initalization();
        }

        public static void Initalization()
        {
            SetDefault();
        }

        public static void SetDefault()
        {
            capacity = 16;
        }
    }
}
