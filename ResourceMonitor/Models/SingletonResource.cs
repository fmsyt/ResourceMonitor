using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceMonitor.Models
{
    internal abstract class SingletonResource<T> where T : SingletonResource<T>, IResource, new() 
    { 
        protected static T _instance = new T();
        public static SingletonResource<T> Instance { get { return _instance; } }
        public abstract float Current();
    }
}
