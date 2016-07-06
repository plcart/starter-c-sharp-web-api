using System.Collections.Generic;

namespace Starter.Domain.Entities
{
    public class Profile:BaseEntity
    {
        public Profile()
        {
            Roles = new HashSet<Role>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
