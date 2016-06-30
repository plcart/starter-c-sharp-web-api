using System.Web.Http;

namespace Starter.Web.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IoCConfig.RegisterDependencies();
            MapperConfig.RegisterMaps();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
