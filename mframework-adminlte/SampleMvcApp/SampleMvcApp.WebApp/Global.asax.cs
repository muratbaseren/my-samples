using Autofac;
using Autofac.Integration.Mvc;
using SampleMvcApp.Core.DataAccess;
using SampleMvcApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SampleMvcApp.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.RegisterMappings();

            var builder = new ContainerBuilder();

            Business.AutoFacModule businessAutoFacModule = new Business.AutoFacModule();
            businessAutoFacModule.RegisterDependencies(builder);

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
