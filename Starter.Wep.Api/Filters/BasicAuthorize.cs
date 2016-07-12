using Starter.Domain.Entities;
using Starter.Domain.Interfaces.Services;
using Starter.Infra.Data.Helpers.Cryptography;
using Starter.Web.Api.Controllers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Starter.Web.Api.Filters
{
    public class BasicAuthorize : AuthorizeAttribute
    {

        Func<HttpActionContext, HttpResponseMessage> UnauthorizedResponse = (context) =>
        {
            var respose = context.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized");
            respose.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic", "realm=\"User Visible Realm\""));
            return respose;
        };

        Func<HttpActionContext, HttpResponseMessage> ForbiddenResponse = (context) =>
            context.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden");

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var auth = actionContext.Request.Headers.Authorization;
            if (auth == null || string.IsNullOrEmpty(auth.Parameter) || auth.Scheme != "Basic")
                actionContext.Response = UnauthorizedResponse(actionContext);
            else
            {
                byte[] data = Convert.FromBase64String(auth.Parameter);
                var values = Encoding.UTF8.GetString(data).Split(':');
                if (values.Count() != 2)
                    actionContext.Response = UnauthorizedResponse(actionContext);
                else
                {
                    var controller = actionContext.ControllerContext.Controller as BaseApiController;
                    var authService = controller.resolver.GetService(typeof(IAuthService)) as IAuthService;
                    var user = authService.Login(values[0], values[1]);
                    if (user == null)
                        actionContext.Response = UnauthorizedResponse(actionContext);
                    else
                    {
                        var roles = user.Profile.Roles.Select(x => x.Name).ToArray();
                        var avaliableRoles = Roles.Split(',');
                        if (!string.IsNullOrEmpty(Roles) 
                            && avaliableRoles.Count() > 0 &&
                            !roles.Any(x => avaliableRoles.Contains(x)))
                            actionContext.Response = ForbiddenResponse(actionContext);
                        else
                            actionContext.RequestContext.Principal = new GenericPrincipal(new GenericIdentity(user.Username), roles);

                    }

                }
            }
        }

    }
}