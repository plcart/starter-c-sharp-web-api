using Starter.Domain.Entities;

namespace Starter.Web.Api.Models
{
    public class PageHighlightModel:BaseEntityModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Language Language { get; set; }
        public MediaType MediaType { get; set; }
        public string MediaValue { get; set; }
        public long PageTitleId { get; set; }
    }
}