using Starter.Web.Api.Models;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dependencies;

namespace Starter.Web.Api.Controllers
{
    [EnableCors("*", "*", "GET,POST,PUT,DELETE,OPTIONS,HEAD")]
    public class BaseApiController : ApiController
    {
        public IDependencyScope resolver { get; } = GlobalConfiguration.Configuration.DependencyResolver.BeginScope();

        public BaseApiController()
        {

        }

        [HttpPost]
        [Route("api/upload")]
        public IHttpActionResult Upload(FileUpload form)
        {
            return Ok(form.Files);
        }

        [NonAction]
        public void ChangeFileLocation(string fileName,string dest)
        {
            string root = HttpContext.Current.Server.MapPath($"~/uploads/temp");
            File.Move($"{root}/{fileName}", $"{dest}/{fileName}");
        }
        
    }
}
