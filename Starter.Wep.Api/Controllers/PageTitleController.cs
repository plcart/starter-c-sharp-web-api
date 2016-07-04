using AutoMapper;
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

        public PageTitleController()
        {
            pageService = resolver.GetService(typeof(IServiceBase<PageTitle>)) as IServiceBase<PageTitle>;
        }

        [HttpGet]
        [Route("api/pages")]
        public IHttpActionResult Get()
        {
            var entities = pageService.GetAll();
            return Ok(Mapper.Map<List<PageTitleModel>>(entities));
        }

        [HttpGet]
        [Route("api/pages/{page}")]
        public IHttpActionResult Get(Page page)
        {
            var entity = pageService.Get(p => p.Page == page);
            return Ok(Mapper.Map<PageTitleModel>(entity));
        }

        [HttpPost]
        [Route("api/pages")]
        [ValidateModel("model")]
        public IHttpActionResult Post(PageTitleModel model)
        {
            var entity = Mapper.Map<PageTitle>(model);
            pageService.Add(entity);
            return Created($"http://{Request.RequestUri.Authority}/api/pages/{entity.Page}".ToLower(), Mapper.Map<PageTitleModel>(entity));
        }

        [HttpPut]
        [Route("api/pages/{page}")]
        [ValidateModel("model")]
        public IHttpActionResult Put(Page page,PageTitleModel model)
        {
            var entity = pageService.Get(p => p.Page == page);
            Mapper.Map(model, entity, typeof(PageTitleModel), typeof(PageTitle));
            pageService.Update(entity);
            return Ok(Mapper.Map<PageTitleModel>(entity));
        }

        [HttpDelete]
        [Route("api/pages/{page}")]
        public IHttpActionResult Delete(Page page)
        {
            var entity = pageService.Get(p => p.Page == page);
            pageService.Remove(entity);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("api/pages/")]
        public IHttpActionResult Delete()
        {
            pageService.RemoveRange();
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
