using Starter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Infra.Data.Context.Configuration
{
    public class RoleMap : BaseEntityMap<Role>
    {
        public RoleMap()
            : base("role")
        {
            Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();
            Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(500)
                .IsOptional();

            Property(x => x.ProfileId)
                .HasColumnName("profile_id")
                .HasColumnType("bigint")
                .IsRequired();

            HasRequired(x => x.Profile)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.ProfileId);
        }
    }
}
