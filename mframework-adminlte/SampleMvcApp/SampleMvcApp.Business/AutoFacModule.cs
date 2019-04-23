using Autofac;
using SampleMvcApp.Business.Abstract;
using SampleMvcApp.Business.Concrete;
using SampleMvcApp.Entities.Concrete;

namespace SampleMvcApp.Business
{
    public class AutoFacModule : Module
    {
        public void RegisterDependencies(ContainerBuilder builder)
        {
            DataAccess.AutoFacModule dataAccessAutoFacModule = new DataAccess.AutoFacModule();
            dataAccessAutoFacModule.RegisterDependencies(builder);

            //builder.RegisterType<EntityManager<Member>>().As<IEntityManager<Member>>().InstancePerLifetimeScope();
            builder.RegisterType<EntityManager<Member>>().As<IEntityManager<Member>>().SingleInstance();
            builder.RegisterType<EntityManager<Customer>>().As<IEntityManager<Customer>>().SingleInstance();
            builder.RegisterType<EntityManager<Department>>().As<IEntityManager<Department>>().SingleInstance();
        }
    }
}
