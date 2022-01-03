using Newtonsoft.Json.Linq;
using ResourceMonitor.Models.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ResourceMonitor.Models
{
    internal class Config
    {
        protected static Config _instance = new();

        public static Config Instance { get { return _instance; } }

        protected static readonly Dictionary<Object, ConfigResource> _resource;
        protected readonly ConfigGeneral configGeneral = new ConfigGeneral();
        protected readonly ConfigResource configResourceCpu = new ConfigResource(nameof(Resources.Cpu));
        protected readonly ConfigResource configResourceMemory = new ConfigResource(nameof(Resources.Memory));

        public static Dictionary<Object, ConfigResource> Resource { get { return _resource; } }
        public ConfigGeneral General { get { return configGeneral; } }
        public ConfigResource Cpu { get { return configResourceCpu; } }
        public ConfigResource Memory { get { return configResourceMemory; } }

        static Config()
        {
            _resource = new Dictionary<Object, ConfigResource>()
            {
                { Resources.Cpu.Instance, _instance.configResourceCpu },
                { Resources.Memory.Instance, _instance.configResourceMemory },
            };
        }

        private Config()
        {
            configResourceMemory.Label = "Mem";
            Initalization();
        }

        public void Initalization()
        {
            Cpu.Load();
            Memory.Load();
            General.Load();
        }

        public void Save()
        {
            Cpu.Save();
            Memory.Save();
            General.Save();
        }
    }

    class ConfigBase
    {
        protected string? Name { get; set; }
        protected string SaveDir { get; set; } = @"config";
        public ConfigBase() { }
        public ConfigBase(string name)
        {
            Name = name;
        }

        public void Save()
        {
            Save(this);
        }

        public static void Save<T>(T instance) where T : ConfigBase
        {
            var _instance = instance.MemberwiseClone();

            var saveDir = instance.SaveDir;
            var filePath = string.Format(@"{0}/{1}.json", saveDir, instance.Name);

            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(_instance, options);

            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }

            var encoding = Encoding.GetEncoding("utf-8");
            using (var writer = new StreamWriter(filePath, false, encoding))
            {
                writer.WriteLine(jsonString);
            }
        }

        public void Load()
        {
            var saveDir = SaveDir;
            var filePath = string.Format(@"{0}/{1}.json", saveDir, Name);

            Load(this, filePath);
        }

        public static void Load<T>(T instance, string filePath) where T : class
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("utf-8"));
            string jsonString = sr.ReadToEnd();
            sr.Close();


            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };

            JObject weatherForecast = JObject.Parse(jsonString);
            foreach (var prop in instance.GetType().GetProperties())
            {
                var _v = weatherForecast[prop.Name];
                var v = Convert.ChangeType(_v, prop.PropertyType);
                prop.SetValue(instance, v);
            }

            // Console.WriteLine(jsonString);

        }
    }

    class ConfigGeneral : ConfigBase
    {
        [JsonInclude]
        public bool Topmost { get; set; }
        public ConfigGeneral()
        {
            Name = "General";
        }
    }

    class ConfigResource : ConfigBase
    {
        public string Label { get; set; }
        [JsonInclude]
        public int Capacity { get; set; } = 60;
        [JsonInclude]
        public string Format { get; set; } = "0%";

        public ConfigResource(string name) : base(name)
        {
            SaveDir = @"config/resources";
            Label = name;
        }
    }
}
