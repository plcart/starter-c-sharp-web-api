using Starter.Web.Api.Models;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Starter.Web.Api.Binders
{
    public class PaginateModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var values = actionContext.ControllerContext.Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);
            string page, items;
            values.TryGetValue("page", out page);
            values.TryGetValue("items", out items);
            bindingContext.Model = new Paginate(page, items);

            return true;
        }
    }
}