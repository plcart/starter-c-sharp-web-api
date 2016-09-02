using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Starter.Web.Api.Startup))]
namespace Starter.Web.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
