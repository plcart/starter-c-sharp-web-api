using AutoMapper;
using Starter.Domain.Entities;
using Starter.Domain.Interfaces.Services;
using Starter.Web.Api.Filters;
using Starter.Web.Api.Models;
using System.Collections.Generic;
using System.Linq;
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
        [HttpOptions]
        [Route("api/pages/{page}/{language}/highlights")]
        public IHttpActionResult Get(Page page,Language language, Paginate p)
        {
            var entities = highlightService.GetAll(x => x.PageTitle.Page == page && x.PageTitle.Language ==language, null, p.Order, p.Reverse, p.ItemsPerPage * p.Page, p.ItemsPerPage);
            return Ok(Mapper.Map<List<PageHighlightModel>>(entities));
        }

        [HttpGet]
        [HttpOptions]
        [Route("api/pages/{page}/{language}/highlights/{id}")]
        public IHttpActionResult Get(Page page, Language language, long id)
        {
            var entity = highlightService.Get(x => x.PageTitle.Page == page && x.PageTitle.Language == language && x.Id == id);
            return Ok(Mapper.Map<PageHighlightModel>(entity));
        }

        [HttpPost]
        [Route("api/pages/{page}/{language}/highlights")]
        [ValidateModel("model")]
        public IHttpActionResult Post(Page page, Language language, PageHighlightModel model)
        {
            var parent = pageService.Get(x => x.Page == page && x.Language == language);
            if (parent == null)
                return NotFound();

            var entity = Mapper.Map<PageHighlight>(model);

            if (!string.IsNullOrEmpty(model.MediaValue))
                entity.MediaValue = model.MediaValue;

            if (!string.IsNullOrEmpty(entity.MediaValue) && 
                ( entity.MediaType == MediaType.Image
                || entity.MediaType == MediaType.File))
            {
                var file = model.MediaValue.Split(';').First();
                ChangeFileLocation(file, HttpContext.Current.Server.MapPath($"~/uploads/pagehighlight"));
                entity.MediaValue = "uploads/pagehighlight/" + file;
            }
            parent.PageHighlights.Add(entity);
            pageService.Update(parent);
            return Created($"http://{Request.RequestUri.Authority}/api/pages/{parent.Page}/highlights/{model.Id}",
                Mapper.Map<PageHighlightModel>(entity));
        }

        [HttpPut]
        [Route("api/pages/{page}/{language}/highlights/{id}")]
        [ValidateModel("model")]
        public IHttpActionResult Put(Page page, Language language, long id, PageHighlightModel model)
        {
            var entity = highlightService.Get(x => x.PageTitle.Page == page && x.PageTitle.Language == language && x.Id == id);
            if (entity == null)
                return NotFound();

            Mapper.Map(model, entity, typeof(PageHighlightModel), typeof(PageHighlight));

            if (model.MediaChange && !string.IsNullOrEmpty(model.MediaValue))
                entity.MediaValue = model.MediaValue;

            if (model.MediaChange && !string.IsNullOrEmpty(entity.MediaValue) &&
                (entity.MediaType == MediaType.Image
                || entity.MediaType == MediaType.File))
            {
                var file = model.MediaValue.Split(';').First();
                ChangeFileLocation(file, HttpContext.Current.Server.MapPath($"~/uploads/pagehighlight"));
                entity.MediaValue = "uploads/pagehighlight/" + file;
            }

            highlightService.Update(entity);
            return Ok(Mapper.Map<PageHighlightModel>(entity));
        }

        [HttpDelete]
        [Route("api/pages/{page}/{language}/highlights/{id}")]
        public IHttpActionResult Delete(Page page, Language language, long id)
        {
            var entity = highlightService.Get(p => p.PageTitle.Page == page && p.PageTitle.Language == language && p.Id==id);
            highlightService.Remove(entity);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("api/pages/{page}/{language}/highlights")]
        public IHttpActionResult Delete(Page page,Language language)
        {
            highlightService.RemoveRange(x => x.PageTitle.Page == page && x.PageTitle.Language == language);
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
