using Starter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Infra.Data.Context.Configuration
{
    public class UserMap : BaseEntityMap<User>
    {
        public UserMap()
            : base("user")
        {
            Property(x => x.Email)
                .HasColumnName("email")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(150)
                .IsRequired();

            Property(x => x.Password)
                .HasColumnName("password")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Username)
                .HasColumnName("username")
                .HasMaxLength(150)
                .IsRequired();

            Property(x => x.ProfileId)
                .HasColumnName("profile_id")
                .HasColumnType("bigint")
                .IsRequired();

            HasRequired(x => x.Profile)
                .WithMany()
                .HasForeignKey(x => x.ProfileId);
            
        }
    }
}
