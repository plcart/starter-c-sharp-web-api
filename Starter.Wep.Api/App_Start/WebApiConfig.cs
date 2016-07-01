using CacheCow.Server;
using Starter.Web.Api.Binders;
using Starter.Web.Api.Models;
using System.Web.Http;

namespace Starter.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.BindParameter(typeof(Paginate), new PaginateModelBinder());

            var cachecow = new CachingHandler(config);
            config.MessageHandlers.Add(cachecow);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
