using Starter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Infra.Data.Context.Configuration
{
    public class ProfileMap : BaseEntityMap<Profile>
    {
        public ProfileMap()
            : base("profile")
        {
            Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();
            Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(500)
                .IsOptional();
        }
    }
}
