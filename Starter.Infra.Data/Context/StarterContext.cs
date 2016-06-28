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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<bool>()
                .Configure(p => p.HasColumnType("bit"));
        }
    }
}
