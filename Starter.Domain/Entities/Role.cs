namespace Starter.Domain.Entities
{
    public class Role:BaseEntity
    {
        public Role():base()
        {

        }
        public string Name { get; set; }
        public string Description { get; set; }

        public long ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
