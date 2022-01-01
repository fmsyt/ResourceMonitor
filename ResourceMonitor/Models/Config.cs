using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models
{
    internal class Config
    {
        public static int Capacity { get; set; } = 60;
        public static string LabelFormat { get; set; } = "0%";

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
            Capacity = 60;
        }
    }
}
