using Autofac;
using Autofac.Integration.WebApi;
using Starter.Domain.Interfaces.Repositories;
using Starter.Domain.Interfaces.Services;
using Starter.Domain.Services;
using Starter.Infra.Data.Repositories;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Starter.Web.Api
{
    public class IoCConfig
    {
        public static void RegisterDependencies()
        {
            var config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterAssemblyTypes(Assembly.Load("Starter.Domain"))
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(Assembly.Load("Starter.Infra.Data"))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(RepositoryBase<>))
               .As(typeof(IRepositoryBase<>))
               .InstancePerRequest();

            builder.RegisterGeneric(typeof(ServiceBase<>))
                .As(typeof(IServiceBase<>))
                .InstancePerRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}