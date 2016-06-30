using Starter.Domain.Entities;

namespace Starter.Infra.Data.Context.Configuration
{
    public class PageHighlightMap:BaseEntityMap<PageHighlight>
    {
        public PageHighlightMap()
            :base("page_highlight")
        {
            Property(x => x.PageTitleId)
                .HasColumnName("page_title_id")
                .HasColumnType("bigint")
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

            HasRequired(x => x.PageTitle)
                .WithMany(x => x.PageHighlights)
                .HasForeignKey(x => x.PageTitleId);
        }
    }
}
