using System.Collections.Generic;
using System.Web.Http.Filters;

namespace Starter.Web.Api.Filters
{
    public class PaginateHeader : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            IEnumerable<string> values;
            if (actionExecutedContext.Request.Headers.TryGetValues("X-Total", out values))
                actionExecutedContext.Response.Headers.Add("X-Total", values);
        }
    }
}