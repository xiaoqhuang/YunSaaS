using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Core.Web;
using Microsoft.Practices.Unity;
using WebGrease.Css.Extensions;

namespace PlatformSite
{
    public class WebInitializer
    {
        public static void Init(IUnityContainer container)
        {
            ControllerResolver controllerResolver = new ControllerResolver(container);
            DependencyResolver.SetResolver(controllerResolver);
            RegisterController(container);
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