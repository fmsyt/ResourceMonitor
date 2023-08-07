using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models
{
    internal abstract class Resource<T> where T : Resource<T>, new() 
    {
        protected static T _instance = new();
        public static Resource<T> Instance { get { return _instance; } }
        public int Count { get; set; } = 0;
        public abstract float Current();
        
    }
}
