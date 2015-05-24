using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Core.Web
{
    public class ControllerResolver : IDependencyResolver
    {
        private readonly IUnityContainer container;

        public ControllerResolver(IUnityContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            if (!container.IsRegistered(serviceType))
                return null;
            return container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (!container.IsRegistered(serviceType))
                return new Collection<object>();
            return container.ResolveAll(serviceType);
        }
    }
}
