using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Starter.Infra.Data.Helpers.Extensions;
using Starter.Web.Api.Formatters.Token;
using Starter.Web.Api.Providers;
using System;
using System.Configuration;

namespace Starter.Web.Api
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var secret = ConfigurationManager.AppSettings["secret"].ToByteArray();

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/api/login"),
                Provider = new ApplicationOAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true,
                AccessTokenFormat = new CustomJwtFormatter("http://localhost:64758")
            });

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions()
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences=  new[] { "audience" },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider("http://localhost:64758",secret)
                }
            });

        }
    }
}