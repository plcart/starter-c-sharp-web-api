using Microsoft.Owin;
using NLog;
using NLog.Owin.Logging;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Starter.Web.Api.Startup))]
namespace Starter.Web.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNLog((eventType) => LogLevel.Error);
        }
    }
}