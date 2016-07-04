using Starter.Web.Api.Filters;
using Starter.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Starter.Web.Api.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("get/")]
        public IHttpActionResult Get(Paginate p)
        {
            
            return Ok();
        }

        [HttpPost]
        [Route("post/")]
        public IHttpActionResult post(FileUpload p)
        {

            return Ok();
        }

    }
}
