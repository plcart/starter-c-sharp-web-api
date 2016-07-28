using Starter.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Starter.Web.Api.Models
{
    public class PageHighlightModel:BaseEntityModel
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string MediaType { get; set; }
        [MaxLength(256)]
        public string MediaValue { get; set; }
        public long PageTitleId { get; set; }
    }
}