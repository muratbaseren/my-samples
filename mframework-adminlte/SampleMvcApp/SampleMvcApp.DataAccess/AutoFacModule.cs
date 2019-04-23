using Autofac;
using SampleMvcApp.Core.DataAccess;
using SampleMvcApp.DataAccess.Concrete;
using SampleMvcApp.Entities.Concrete;
using System.Data.Entity;

namespace SampleMvcApp.DataAccess
{
    public class AutoFacModule : Module
    {
        public void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseContext>().As<DbContext>();
            builder.RegisterType<Repository<Member>>().As<IDataAccess<Member>>().SingleInstance();
            builder.RegisterType<Repository<Customer>>().As<IDataAccess<Customer>>().SingleInstance();
            builder.RegisterType<Repository<Department>>().As<IDataAccess<Department>>().SingleInstance();
        }
    }
}
