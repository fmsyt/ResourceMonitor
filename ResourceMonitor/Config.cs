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
        protected readonly ConfigGeneral configGeneral = new();
        public ConfigGeneral General { get { return configGeneral; } }
        private Config() {}
    }
}