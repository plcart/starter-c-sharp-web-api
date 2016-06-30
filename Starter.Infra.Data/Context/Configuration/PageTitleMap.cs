using Starter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Infra.Data.Context.Configuration
{
    public class PageTitleMap : BaseEntityMap<PageTitle>
    {
        public PageTitleMap()
            : base("page_title")
        {

            Property(x => x.Page)
               .HasColumnName("page")
               .HasColumnType("int")
               .IsRequired();
            Property(x => x.Title)
                .HasColumnName("title")
                .HasMaxLength(150)
                .IsRequired();
            Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(500)
                .IsOptional();
            Property(x => x.Language)
                .HasColumnName("language")
                .HasColumnType("int")
                .IsRequired();
            Property(x => x.MediaType)
                .HasColumnName("media_type")
                .HasColumnType("int")
                .IsRequired();
            Property(x => x.MediaValue)
                .HasColumnName("media_value")
                .HasMaxLength(256)
                .IsOptional();

        }
    }
}
