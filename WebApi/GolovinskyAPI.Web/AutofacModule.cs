using Autofac;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Logic.Handlers;
using GolovinskyAPI.Logic.Interfaces;
using System.Linq;

namespace GolovinskyAPI.Web
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IBaseRepository).Assembly)
            .Where(t => typeof(IBaseRepository).IsAssignableFrom(t))
            .AsImplementedInterfaces()
            .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(IBaseService).Assembly)
            .Where(t => typeof(IBaseService).IsAssignableFrom(t))
            .AsImplementedInterfaces()
            .InstancePerDependency();

            builder.RegisterType<AuthHandler>()
                .As<IAuthHandler>();
            builder.RegisterType<UploadPictureHandler>()
                .As<IUploadPicture>();
        }
    }
}