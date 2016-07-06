namespace Starter.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public long ProfileId { get; set; }
        public virtual Profile Profile { get;set;}
    }
}
