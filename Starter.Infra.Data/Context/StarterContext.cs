using Starter.Domain.Entities;
using Starter.Infra.Data.Context.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Starter.Infra.Data.Context
{
    public class StarterContext : DbContext
    {
        public StarterContext()
            : base("name=StarterConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<PageTitle> PageTitles { get;}
        public DbSet<PageHighlight> PageHighlights { get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<bool>()
                .Configure(p => p.HasColumnType("bit"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(256));

            modelBuilder.Configurations.Add(new PageTitleMap());
            modelBuilder.Configurations.Add(new PageHighlightMap());
        }
    }
}
