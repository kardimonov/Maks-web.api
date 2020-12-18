using Autofac;
using MobileApi.Data.Interfaces;
using MobileApi.Logic.Interfaces;
using System.Linq;

namespace MobileApi.Web
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IRepository).Assembly)
            .Where(t => typeof(IRepository).IsAssignableFrom(t))
            .AsImplementedInterfaces()
            .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(IService).Assembly)
            .Where(t => typeof(IService).IsAssignableFrom(t))
            .AsImplementedInterfaces()
            .InstancePerDependency();
        }
    }
}