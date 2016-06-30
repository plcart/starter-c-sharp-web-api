using Starter.Domain.Entities;
using Starter.Domain.Interfaces.Services;
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

        public PageTitleController()
        {
            pageService = resolver.GetService(typeof(IServiceBase<PageTitle>)) as IServiceBase<PageTitle>;
        }

        [HttpGet]
        [Route("api/pages")]
        public IHttpActionResult Get()
        {
            return Ok();
        }

    }
}
