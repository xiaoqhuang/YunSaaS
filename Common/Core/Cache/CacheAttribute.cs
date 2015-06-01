using System;

namespace Core.Cache
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute : Attribute
    {
        public CacheAttribute(CacheType cacheType, string key)
        {
            Key = key;
            CacheType = cacheType;
        }

        public CacheAttribute(CacheType cacheType, string key, int durationMinutes) : this(cacheType, key)
        {
            DurationMinutes = durationMinutes;
        }

        public CacheAttribute(CacheType cacheType, string key, bool isRemoveCache)
            : this(cacheType, key)
        {
            IsRemoveCache = isRemoveCache;
        }

        /// <summary>
        /// Cache类型
        /// </summary>
        public CacheType CacheType { get; set; }
        /// <summary>
        /// CacheKey
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Cache持续时间
        /// </summary>
        public int? DurationMinutes { get; set; }
        /// <summary>
        /// 是否自动清除Cache
        /// </summary>
        public bool IsRemoveCache { get; set; }
    }

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public class CacheKeyAttribute : Attribute
    {

    }

    public enum CacheType
    {
        Memory,
        Session,
        Distributed
    }
}