using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using Starter.Domain.Interfaces.Services;
using Microsoft.Owin.Security;

namespace Starter.Web.Api.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext c)
        {
            var authService = GlobalConfiguration.Configuration.DependencyResolver.BeginScope().GetService(typeof(IAuthService)) as IAuthService;
            var user = authService.Login(c.UserName, c.Password);
            if (user!=null)
            {
                Claim claim1 = new Claim(ClaimTypes.Name, c.UserName);
                
                var claims = new Claim[] { claim1 };
                claims = claims.Concat(user.Profile.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name))).ToArray();
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(
                       claims, OAuthDefaults.AuthenticationType);
                var ticket = new AuthenticationTicket(claimsIdentity, new AuthenticationProperties());
                c.Validated(ticket);
            }

            return Task.FromResult<object>(null);
        }
    }
}