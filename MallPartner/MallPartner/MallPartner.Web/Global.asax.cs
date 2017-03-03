using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using StMallB.Service;
using StMallB.Service.Impl;
using StMallB.Repository;
using StMallB.Repository.Impl;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using StMallB.Repository.DbContextFactory;
using StMallB.Repository.DbContextFactory.Impl;
using StMallB.Repository.UnitOfWork;
using StMallB.Repository.UnitOfWork.Impl;
using System.Reflection;

namespace StMallB.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Initialise();
        }

        IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IDbContextFactory, DbContextFactory>(
                     new HierarchicalLifetimeManager(),
                     new InjectionConstructor("DefaultConnection"));

            #region Repositories
            container.RegisterType<IJoinBusinessRepository, JoinBusinessRepositoryImpl>();
            container.RegisterType<IMemberIntegralRepository, MemberIntegralRepositoryImpl>();
            container.RegisterType<IMemberIntegralRecordRepository, MemberIntegralRecordRepositoryImpl>();
            container.RegisterType<IChangePswRepository, ChangePswRepositoryImpl>();
            container.RegisterType<IJoinBusinessRechargeRepository, JoinBusinessRechargeRepositoryImpl>();
            #endregion

            #region services
            container.RegisterType<IJoinBusinessService, JoinBusinessServiceImpl>();
            container.RegisterType<IMemberIntegralService, MemberIntegralServiceImpl>();
            container.RegisterType<IChangePswService, ChangePswServiceImpl>();
            container.RegisterType<IJoinBusinessRechargeService, JoinBusinessRechargeServiceImpl>();

            #endregion

            return container;
        }

        void Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
