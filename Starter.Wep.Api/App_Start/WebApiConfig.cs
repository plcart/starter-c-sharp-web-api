﻿using CacheCow.Server;
using Starter.Web.Api.Binders;
using Starter.Web.Api.Filters;
using Starter.Web.Api.Models;
using System.Web.Http;

namespace Starter.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.Filters.Add(new CorsOptions());

            config.BindParameter(typeof(Paginate), new PaginateModelBinder());
            config.BindParameter(typeof(FileUpload), new FileUploadModelBinder());


            var cachecow = new CachingHandler(config);
            config.MessageHandlers.Add(cachecow);

            FormatterConfig.RegisterFormatters(config);

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
