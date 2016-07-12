using AutoMapper;
using Starter.Domain.Entities;
using Starter.Domain.Interfaces.Services;
using Starter.Web.Api.Filters;
using Starter.Web.Api.Models;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Starter.Web.Api.Controllers
{
    public class PageHighlightController : BaseApiController
    {
        IServiceBase<PageTitle> pageService { get; }
        IServiceBase<PageHighlight> highlightService { get; }

        public PageHighlightController()
        {
            pageService = resolver.GetService(typeof(IServiceBase<PageTitle>)) as IServiceBase<PageTitle>;
            highlightService = resolver.GetService(typeof(IServiceBase<PageHighlight>)) as IServiceBase<PageHighlight>;
        }

        [HttpGet]
        [Route("api/pages/{page}/highlights")]
        public IHttpActionResult Get(Page page, Paginate p)
        {
            var entities = highlightService.GetAll(x => x.PageTitle.Page == page, null, p.Order, p.Reverse, p.ItemsPerPage * p.Page, p.ItemsPerPage);
            return Ok(Mapper.Map<List<PageHighlightModel>>(entities));
        }

        [HttpGet]
        [Route("api/pages/{page}/highlights/{id}")]
        public IHttpActionResult Get(Page page, long id)
        {
            var entity = highlightService.Get(x => x.PageTitle.Page == page && x.Id == id);
            return Ok(Mapper.Map<PageHighlightModel>(entity));
        }

        [HttpPost]
        [Route("api/pages/{page}/highlights")]
        [ValidateModel("model")]
        public IHttpActionResult Post(Page page, PageHighlightModel model)
        {
            var parent = pageService.Get(x => x.Page == page);
            if (parent == null)
                return NotFound();

            var entity = Mapper.Map<PageHighlight>(model);
            if (model.MediaType == MediaType.Image
                || model.MediaType == MediaType.File)
                ChangeFileLocation(model.MediaValue, HttpContext.Current.Server.MapPath($"~/uploads/pagehighlight"));
            parent.PageHighlights.Add(entity);
            pageService.Update(parent);
            return Created($"http://{Request.RequestUri.Authority}/api/pages/{parent.Page}/highlights/{model.Id}",
                Mapper.Map<PageHighlightModel>(entity));
        }

        [HttpPut]
        [Route("api/pages/{page}/highlights/{id}")]
        [ValidateModel("model")]
        public IHttpActionResult Put(Page page, long id, PageHighlightModel model)
        {
            var entity = highlightService.Get(x => x.PageTitle.Page == page && x.Id == id);
            if (entity == null)
                return NotFound();

            Mapper.Map(model, entity, typeof(PageHighlightModel), typeof(PageHighlight));
            if (model.MediaType == MediaType.Image
                || model.MediaType == MediaType.File)
                ChangeFileLocation(model.MediaValue, HttpContext.Current.Server.MapPath($"~/uploads/pagehighlight"));

            highlightService.Update(entity);
            return Ok(Mapper.Map<PageHighlightModel>(entity));
        }

        [HttpDelete]
        [Route("api/pages/{page}/highlights/{id}")]
        public IHttpActionResult Delete(Page page,long id)
        {
            var entity = highlightService.Get(p => p.PageTitle.Page == page && p.Id==id);
            highlightService.Remove(entity);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("api/pages/{page}/highlights")]
        public IHttpActionResult Delete(Page page)
        {
            highlightService.RemoveRange(x => x.PageTitle.Page == page);
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
