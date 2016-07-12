using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Starter.Web.Api.Models
{
    public class ProfileModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public List<RoleModel> Roles { get; set; }
    }
}