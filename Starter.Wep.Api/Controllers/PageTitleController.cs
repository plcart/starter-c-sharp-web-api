using AutoMapper;
using Starter.Domain.Entities;
using Starter.Domain.Interfaces.Services;
using Starter.Web.Api.Filters;
using Starter.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Starter.Web.Api.Controllers
{
    public class PageTitleController : BaseApiController
    {

        IServiceBase<PageTitle> pageService { get; }

        public PageTitleController()
        {
            pageService = resolver.GetService(typeof(IServiceBase<PageTitle>)) as IServiceBase<PageTitle>;
        }

        public PageTitleController(IServiceBase<PageTitle> p)
        {
            pageService = p;
        }

        [HttpGet]
        [Route("api/pages")]
        public IHttpActionResult Get(Paginate p)
        {
            var entities = pageService.GetAll(null, p.Order, p.Reverse, p.Page * p.ItemsPerPage, p.ItemsPerPage);
            return Ok(Mapper.Map<List<PageTitleModel>>(entities));
        }

        [HttpGet]
        [Route("api/pages/{page}")]
        public IHttpActionResult Get(Page page)
        {
            var entity = pageService.Get(p => p.Page == page, null);
            return Ok(Mapper.Map<PageTitleModel>(entity));
        }

        [HttpPost]
        [Route("api/pages")]
        [ValidateModel("model")]
        public IHttpActionResult Post(PageTitleModel model)
        {
            var entity = Mapper.Map<PageTitle>(model);
            if (model.MediaType == MediaType.Image
                || model.MediaType == MediaType.File)
                ChangeFileLocation(model.MediaValue, HttpContext.Current.Server.MapPath($"~/uploads/pagetitle"));
            pageService.Add(entity);
            return Created($"http://{Request.RequestUri.Authority}/api/pages/{entity.Page}".ToLower(), Mapper.Map<PageTitleModel>(entity));
        }

        [HttpPut]
        [Route("api/pages/{page}")]
        [ValidateModel("model")]
        public IHttpActionResult Put(Page page, PageTitleModel model)
        {
            var entity = pageService.Get(p => p.Page == page);
            Mapper.Map(model, entity, typeof(PageTitleModel), typeof(PageTitle));
            if (model.MediaType == MediaType.Image
                || model.MediaType == MediaType.File)
                ChangeFileLocation(model.MediaValue, HttpContext.Current.Server.MapPath($"~/uploads/pagetitle"));
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
