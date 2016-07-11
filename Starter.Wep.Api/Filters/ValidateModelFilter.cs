using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Linq;

namespace Starter.Web.Api.Filters
{
    public class ValidateModel : ActionFilterAttribute
    {
        string[] Names { get; }

        public ValidateModel() { }

        public ValidateModel(params string[] names)
        {
            Names = names;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
                actionContext.Response = actionContext.Request
                     .CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            else if (actionContext.ActionArguments
                .Where(x => Names.Contains(x.Key) && x.Value == null).Count() > 0)
                actionContext.Response = actionContext.Request
                    .CreateErrorResponse(HttpStatusCode.BadRequest, "The request is invalid.");
        }

    }
}