using System;

namespace Starter.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Created = DateTime.Now;
        }

        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
