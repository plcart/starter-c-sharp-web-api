using System.ComponentModel.DataAnnotations;

namespace Starter.Web.Api.Models
{
    public class RoleModel:BaseEntityModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }
}