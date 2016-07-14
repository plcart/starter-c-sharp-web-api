using Microsoft.Owin.Security.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Http;
using Starter.Domain.Interfaces.Services;
using System.Linq.Expressions;
using Starter.Domain.Entities;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;

namespace Starter.Web.Api.Providers
{
    public class FacebookOAuthProvider : FacebookAuthenticationProvider
    {
        public override void ApplyRedirect(FacebookApplyRedirectContext context)
        {
            base.ApplyRedirect(context);
        }
        public override Task ReturnEndpoint(FacebookReturnEndpointContext context)
        {
            return base.ReturnEndpoint(context);
        }

        public override Task Authenticated(FacebookAuthenticatedContext context)
        {
            var authService = GlobalConfiguration.Configuration.DependencyResolver.BeginScope().GetService(typeof(IAuthService)) as IAuthService;
            var user = authService.Get(x => x.Username == context.UserName, new Expression<Func<User, object>>[]
                {x=>x.Profile.Roles });
            if (user != null)
            {
                Claim claim1 = new Claim(ClaimTypes.Name, context.UserName);

                var claims = new Claim[] { claim1 };
                claims = claims.Concat(user.Profile.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name))).ToArray();
                context.Identity =  new ClaimsIdentity(
                       claims, OAuthDefaults.AuthenticationType);
            }
            else
            {
                authService.Add(new User()
                {
                    Email = context.Email,
                    Name = context.Name,
                    Password = "Ab123456",
                    Username = context.UserName,
                    ProfileId = 1
                });
                user = authService.Get(x => x.Username == context.UserName, new Expression<Func<User, object>>[]
                {x=>x.Profile.Roles });

                Claim claim1 = new Claim(ClaimTypes.Name, context.UserName);

                var claims = new Claim[] { claim1 };
                claims = claims.Concat(user.Profile.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name))).ToArray();
                context.Identity = new ClaimsIdentity(
                       claims, OAuthDefaults.AuthenticationType);
            }
            return Task.FromResult<object>(null);
        }
    }
}