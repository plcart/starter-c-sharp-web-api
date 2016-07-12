using System.ComponentModel.DataAnnotations;

namespace Starter.Web.Api.Models
{
    public class UserModel:BaseEntityModel
    {
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(150)]
        public string Username { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required]
        [MaxLength(12)]
        [MinLength(8)]
        [RegularExpression(@"^(([a-zA-Z]+\d+)|(\d+[a-zA-Z]+))[a-zA-Z0-9]*$",
            ErrorMessage = "Password must be at least 8 char long and contains at least 1 uppercase letter, 1 lowercase letter and 1 number")]
        public string Password { get; set; }
        public long ProfileId { get; set; }
        public ProfileModel Profile { get; set; }
    }
}