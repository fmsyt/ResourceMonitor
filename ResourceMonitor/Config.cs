using ResourceMonitor.Configs;
using ResourceMonitor.Models;
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

        protected readonly Dictionary<string, ConfigResourceBase> _configDictionary;
        public Dictionary<string, ConfigResourceBase> Resource { get { return _configDictionary; } }

        protected readonly ConfigGeneral configGeneral = new();
        public ConfigGeneral General { get { return configGeneral; } }

        
        private Config()
        {
            _configDictionary = new Dictionary<string, ConfigResourceBase>()
            {
                { "Cpu", new ConfigCpu() },
                { "Memory", new ConfigMemory() },
                { "MemorySwap", new ConfigMemorySwap() },
            };
        }

        public ConfigResourceBase Get(string name)
        {
            if (_configDictionary.ContainsKey(name))
            {
                return _configDictionary[name];
            }

            var resouce = new ConfigResourceBase();
            resouce.Label = name;

            return resouce;
        }

        public ConfigResourceBase FromResource(Resource resouce)
        {
            var typeName = resouce.GetType().Name;
            if (_configDictionary.ContainsKey(typeName))
            {
                return _configDictionary[typeName];
            }

            var baseConfig = new ConfigResourceBase();
            baseConfig.Label = typeName;

            return baseConfig;
        }

        

        public void SaveAll()
        {
            General.Save();
        }
    }
}