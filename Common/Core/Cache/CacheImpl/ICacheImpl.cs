namespace Core.Cache.CacheImpl
{
    public interface ICacheImpl
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Add(string key, object value);

        /// <summary>
        /// 添加缓存，指定缓存持续时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="durationMinutes"></param>
        void Add(string key, object value, int durationMinutes);

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        object Get(string key);

        /// <summary>
        /// 缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Contains(string key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}