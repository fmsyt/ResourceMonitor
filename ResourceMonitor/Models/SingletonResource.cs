using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models
{
    internal abstract class SingletonResource<T> where T : SingletonResource<T>, new() 
    { 
        protected static T _instance = new();
        public static SingletonResource<T> Instance { get { return _instance; } }

        public abstract int Current();
    }
}
