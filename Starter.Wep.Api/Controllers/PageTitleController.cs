using Starter.Domain.Entities;
using Starter.Domain.Interfaces.Services;
using Starter.Web.Api.Filters;
using Starter.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace Starter.Web.Api.Controllers
{
    public class PageTitleController : ApiController
    {
        IDependencyScope resolver { get; } = GlobalConfiguration.Configuration.DependencyResolver.BeginScope();
        IServiceBase<PageTitle> pageService { get; }
        static int Id = 0;
        public PageTitleController()
        {
            pageService = resolver.GetService(typeof(IServiceBase<PageTitle>)) as IServiceBase<PageTitle>;
        }

        [HttpGet]
        [Route("api/pages")]
        public IHttpActionResult Get()
        {
            var s = new PageTitleModel() { Title = "sfasafa" , Id = Id };
            return Ok(s);
        }

        [HttpPost]
        [Route("api/pages")]
        public IHttpActionResult Post()
        {
            var s = new PageTitleModel() { Title = "sfasafa", Id = Id++ };
            return Ok(s);
        }

    }
}
