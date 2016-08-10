using System.Web.Http;
using WebActivatorEx;
using Starter.Web.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Starter.Web.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.IncludeXmlComments(GetXmlCommentsPath());
                        c.SingleApiVersion("v1", "Starter.Web.Api");
                    })
                .EnableSwaggerUi(c =>
                    {

                    });
        }

        private static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\Starter.Web.Api.xml",
                System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
