using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Core.Cache.CacheImpl;
using log4net;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Core.Cache.Interceptor
{
    /// <summary>
    /// Cache拦截器，需要继承IInterceptionBehavior接口
    /// </summary>
    public class CacheInterceptor : IInterceptionBehavior
    {
        private ILog logger = LogManager.GetLogger(typeof(CacheInterceptor));
        [Dependency]
        internal IUnityContainer Container { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            //在执行目标方法之前，先判断有没有定义Cache attribute，如果没有Cache attribute，则直接执行方法并返回
            var cacheAttribute = input.MethodBase.GetCustomAttributes(typeof (CacheAttribute), false).FirstOrDefault() as CacheAttribute;
            if (cacheAttribute == null)
            {
                //没有Cache attribute，直接返回
                return getNext()(input, getNext);
            }

            string strCacheKey = cacheAttribute.Key + GetCacheKey(input);
            if (!cacheAttribute.IsRemoveCache)
            {
                //如果Cache已经存在，直接返回Cache值
                var cacheValue = GetCacheValue(strCacheKey, cacheAttribute.CacheType);
                if (cacheValue != null)
                {
                    logger.InfoFormat("命中{0} cache, key={1}", cacheAttribute.CacheType, strCacheKey);
                    return input.CreateMethodReturn(cacheValue);
                }
            }

            //执行被拦截方法
            var result = getNext()(input, getNext);

            if (cacheAttribute.IsRemoveCache)
            {
                //自动删除Cache
                ClearCache(strCacheKey, cacheAttribute.CacheType);
                logger.InfoFormat("删除{0} cache, key={1}", cacheAttribute.CacheType, strCacheKey);
            }
            else if (result.ReturnValue != null)
            {
                //在执行被拦截方法之后，添加返回值到Cache
                AddToCache(strCacheKey, cacheAttribute.CacheType, result.ReturnValue, cacheAttribute.DurationMinutes);
                logger.InfoFormat("添加到{0} cache, key={1}", cacheAttribute.CacheType, strCacheKey);
            }

            return result;
        }

        private void ClearCache(string cacheKey, CacheType cacheType)
        {
            ICacheImpl cacheImpl = Container.Resolve<ICacheImpl>(cacheType.ToString());
            
            cacheImpl.Remove(cacheKey);
        }

        private void AddToCache(string cacheKey, CacheType cacheType, object value, int? durationMinutes)
        {
            ICacheImpl cacheImpl = Container.Resolve<ICacheImpl>(cacheType.ToString());
            if (durationMinutes != null && durationMinutes.Value > 0)
            {
                cacheImpl.Add(cacheKey, value, durationMinutes.Value);
            }
            else
            {
                cacheImpl.Add(cacheKey, value);
            }
        }

        private object GetCacheValue(string cacheKey, CacheType cacheType)
        {
            ICacheImpl cacheImpl = Container.Resolve<ICacheImpl>(cacheType.ToString());
            return cacheImpl.Get(cacheKey);
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        private string GetCacheKey(IMethodInvocation input)
        {
            StringBuilder cacheKey = new StringBuilder();
            for (int i = 0; i < input.Arguments.Count; i++)
            {
                ParameterInfo parameterInfo = input.Arguments.GetParameterInfo(i);
                if (parameterInfo.GetCustomAttributes(typeof (CacheKeyAttribute)).FirstOrDefault() != null)
                {
                    string strName = parameterInfo.Name;
                    object value = input.Arguments[i];
                    if (!parameterInfo.ParameterType.IsPrimitive)
                    {
                        PropertyInfo propertyInfo = parameterInfo.ParameterType.GetProperties(
                            BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
                            .FirstOrDefault(info => info.GetCustomAttributes(typeof (CacheKeyAttribute), false).Length == 1);
                        if (propertyInfo != null)
                        {
                            strName = parameterInfo.Name + "." + propertyInfo.Name;
                            value = propertyInfo.GetValue(value);
                        }
                    }
                    cacheKey.Append(strName).Append("=").Append(value);
                }
            }
            return cacheKey.ToString();
        }
    }
}