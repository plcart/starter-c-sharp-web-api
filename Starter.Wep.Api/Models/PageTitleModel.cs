using Starter.Domain.Entities;
using System.Collections.Generic;

namespace Starter.Web.Api.Models
{
    public class PageTitleModel:BaseEntityModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Page Page { get; set; }
        public Language Language { get; set; }
        public MediaType MediaType { get; set; }
        public string MediaValue { get; set; }
        public List<PageHighlightModel> PageHighlights { get; set; }
    }
}