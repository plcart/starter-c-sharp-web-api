using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Starter.Web.Api.Filters
{
    public class CorsOptions : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Method.Method == "OPTIONS")
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}