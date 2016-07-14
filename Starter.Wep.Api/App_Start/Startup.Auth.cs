using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Starter.Web.Api.Providers;
using System;

namespace Starter.Web.Api
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(OAuthDefaults.AuthenticationType);

           
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/api/login"),
                Provider = new ApplicationOAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            });

            app.UseFacebookAuthentication(new FacebookAuthenticationOptions()
            {
                AppId = "1710166282560655",
                AppSecret = "33fb4b3454611fdfa0110cce29281080",
                Provider = new FacebookOAuthProvider(),
            });
            
        }
    }
}