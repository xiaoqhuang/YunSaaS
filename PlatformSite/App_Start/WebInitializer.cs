using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Core.Cache;
using Core.Cache.CacheImpl;
using Core.Cache.Interceptor;
using Core.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using WebGrease.Css.Extensions;

namespace PlatformSite
{
    public class WebInitializer
    {
        public static void Init(IUnityContainer container)
        {
            container.RegisterInstance(container, new ContainerControlledLifetimeManager());
            ControllerResolver controllerResolver = new ControllerResolver(container);
            DependencyResolver.SetResolver(controllerResolver);
            container.RegisterInstance("CurrentSchema", "dbo");
            container.RegisterInstance("DefaultCacheDurationMinutes", "60");
            RegistryCache(container);
            RegisterController(container);
        }

        private static void RegistryCache(IUnityContainer container)
        {
            container.AddNewExtension<Interception>();
            container.RegisterType<ICacheImpl, MemoryCacheImpl>("Memory");

            //Cache只加在Service层
            Assembly assembly = Assembly.Load("Service");
            foreach (Type type in assembly.GetTypes())
            {
                foreach (MethodInfo methodInfo in type.GetMethods())
                {
                    //有任何一个方法有CacheAttribute则为类添加Cache拦截器
                    if (methodInfo.GetCustomAttributes(typeof (CacheAttribute), true).Length > 0)
                    {
                        container.RegisterType(type, new Interceptor<VirtualMethodInterceptor>(),
                            new InterceptionBehavior<CacheInterceptor>());
                        break;
                    }
                }
            }
        }

        private static void RegisterController(IUnityContainer container)
        {
            //注册当前程序集下所有Controller
            Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.IsSubclassOf(typeof (Controller)))
                .ForEach(type => container.RegisterType(type));
        }
    }
}