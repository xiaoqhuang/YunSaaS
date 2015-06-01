using System;
using System.Runtime.Caching;
using Microsoft.Practices.Unity;

namespace Core.Cache.CacheImpl
{
    public class MemoryCacheImpl : ICacheImpl
    {
        [Dependency("DefaultCacheDurationMinutes")]
        internal string DefaultCacheDurationMinutes { get; set; }

        private int CacheDurationMinutes
        {
            get
            {
                int durationMinutes = 60;
                int.TryParse(DefaultCacheDurationMinutes, out durationMinutes);
                return durationMinutes;
            }
        }

        public void Add(string key, object value)
        {
            //没有配置DefaultCacheDurationMinutes则默认60分钟
            Add(key, value, CacheDurationMinutes);
        }

        public void Add(string key, object value, int durationMinutes)
        {
            MemoryCache.Default.Set(key, value, DateTime.Now.AddMinutes(durationMinutes));
        }

        public T Get<T>(string key)
        {
            return (T) Get(key);
        }

        public object Get(string key)
        {
            return MemoryCache.Default.Get(key);
        }

        public bool Contains(string key)
        {
            return MemoryCache.Default.Contains(key);
        }

        public void Remove(string key)
        {
            MemoryCache.Default.Remove(key);
        }
    }
}