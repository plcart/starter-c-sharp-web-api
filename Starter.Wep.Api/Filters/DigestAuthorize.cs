using Starter.Domain.Entities;
using Starter.Domain.Interfaces.Services;
using Starter.Infra.Data.Helpers.Cryptography;
using Starter.Web.Api.Controllers;
using System;
using System.Configuration;
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
    public class DigestAuthorize : AuthorizeAttribute
    {
        Func<HttpActionContext, HttpResponseMessage> UnauthorizedResponse = (context) =>
        {
            var respose = context.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized");
            var nonce = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{DateTime.Now.ToString("yyyyMMddmmss")}:{ConfigurationManager.AppSettings["secret"]}"));
            respose.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Digest", $"realm=\"{ConfigurationManager.AppSettings["realm"]}\", nonce=\"{nonce}\""));
            return respose;
        };

        Func<HttpActionContext, HttpResponseMessage> ForbiddenResponse = (context) =>
            context.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden");

        Func<IServiceBase<User>, string, User> retriveUser = (service, username) =>
            service.Get(x => x.Username == username,
                new Expression<Func<User, object>>[] { x => x.Profile, x => x.Profile.Roles });


        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var auth = actionContext.Request.Headers.Authorization;
            if (auth == null || string.IsNullOrEmpty(auth.Parameter) || auth.Scheme != "Digest")
                actionContext.Response = UnauthorizedResponse(actionContext);
            else
            {
                var hValues = auth.Parameter.Split(',').ToDictionary(x => x.Substring(0, x.IndexOf('=')).Trim(),
                    x => x.Substring(x.IndexOf('=') + 1).Replace("\"", ""));
                if (!hValues.ContainsKey("nonce")
                    || !hValues.ContainsKey("uri")
                    || !hValues.ContainsKey("username")
                    || !hValues.ContainsKey("response"))
                    actionContext.Response = UnauthorizedResponse(actionContext);
                else
                {
                    var controller = actionContext.ControllerContext.Controller as BaseApiController;
                    var userService = controller.resolver.GetService(typeof(IAuthService)) as IAuthService;
                    var username = hValues["username"];
                    var user = userService.Get(x => x.Username == username,new Expression<Func<User, object>>[]
                        { x=>x.Profile.Roles});
                    if (user == null)
                        actionContext.Response = UnauthorizedResponse(actionContext);
                    else
                    {
                        var ha2 = MD5.Encrypt($"{actionContext.Request.Method.Method}:{hValues["uri"]}");
                        var response = MD5.Encrypt($"{user.Password}:{hValues["nonce"]}:{ha2}");
                        if (response != hValues["response"])
                            actionContext.Response = UnauthorizedResponse(actionContext);
                        else
                        {
                            var roles = user.Profile.Roles.Select(x => x.Name).ToArray();
                            var avaliableRoles = Roles.Split(',');
                            if (!string.IsNullOrEmpty(Roles) && avaliableRoles.Count() > 0 &&
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
}