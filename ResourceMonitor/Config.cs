using ResourceMonitor.Configs;
using ResourceMonitor.Models.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ResourceMonitor
{
    internal class Config
    {
        protected static Config _instance = new();

        public static Config Instance { get { return _instance; } }

        protected static readonly Dictionary<Object, ConfigResourceBase> _resource;
        protected readonly ConfigGeneral configGeneral = new();
        protected readonly ConfigCpu configCpu = new();
        protected readonly ConfigMemory configMemory = new();

        public static Dictionary<Object, ConfigResourceBase> Resource { get { return _resource; } }
        public ConfigGeneral General { get { return configGeneral; } }
        public ConfigCpu Cpu { get { return configCpu; } }
        public ConfigMemory Memory { get { return configMemory; } }

        static Config()
        {
            _resource = new Dictionary<Object, ConfigResourceBase>()
            {
                { Models.Resources.Cpu.Instance, new ConfigCpu() },
                { Models.Resources.Memory.Instance, new ConfigMemory() },
            };
        }

        private Config()
        {
        }

        public void SaveAll()
        {
            Cpu.Save();
            Memory.Save();
            General.Save();
        }
    }
}