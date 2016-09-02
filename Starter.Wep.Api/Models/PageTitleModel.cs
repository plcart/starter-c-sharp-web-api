using Starter.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Starter.Web.Api.Models
{
    public class PageTitleModel : BaseEntityModel
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public string Page { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string MediaType { get; set; }
        [MaxLength(256)]
        public string MediaValue { get; set; }
        public bool MediaChange { get; set; } = false;
        public List<PageHighlightModel> PageHighlights { get; set; }
    }
}