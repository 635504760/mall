using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace StMallB.Web.App_Start
{
    public class UnityDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        private IUnityContainer container;
        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }
        public object GetService(Type serviceType)
        {
            if (!container.IsRegistered(serviceType))
            {
                return null;
            }
            return container.Resolve(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (!container.IsRegistered(serviceType))
            {
                return null;
            }
            return container.ResolveAll(serviceType);
        }
    }
}