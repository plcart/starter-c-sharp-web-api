using Starter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Starter.Web.Api.Controllers
{
    public class EnumController : BaseApiController
    {
        [HttpGet]
        [HttpOptions]
        [Route("api/enums/pages")]
        public IHttpActionResult GetPages()
        {
            return Ok(Enum.GetValues(typeof(Page)).Cast<Page>().Select(x=> x.ToString()));
        }

        [HttpGet]
        [HttpOptions]
        [Route("api/enums/mediatypes")]
        public IHttpActionResult GetMediaTypes()
        {
            return Ok(Enum.GetValues(typeof(MediaType)).Cast<MediaType>().Select(x => x.ToString()));
        }

        [HttpGet]
        [HttpOptions]
        [Route("api/enums/languages")]
        public IHttpActionResult GetLanguages()
        {
            return Ok(Enum.GetValues(typeof(Language)).Cast<Language>().Select(x => x.ToString()));
        }

    }
}
