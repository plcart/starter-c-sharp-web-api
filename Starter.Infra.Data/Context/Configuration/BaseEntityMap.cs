using Starter.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Starter.Infra.Data.Context.Configuration
{
    public class BaseEntityMap<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
        public BaseEntityMap(string table)
        {
            ToTable(table);
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("bigint")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(x => x.Created)
                .HasColumnName("created")
                .HasColumnType("datetime")
                .IsRequired();

            Property(x => x.Updated)
                .HasColumnName("updated")
                .HasColumnType("datetime")
                .IsOptional();

            Property(x => x.Deleted)
                .HasColumnName("deleted")
                .HasColumnType("datetime")
                .IsOptional();
        }
    }
}
