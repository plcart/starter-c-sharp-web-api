namespace Starter.Infra.Data.Migrations
{
    using Domain.Entities;
    using Helpers.Cryptography;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    internal sealed class Configuration : DbMigrationsConfiguration<Context.StarterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Context.StarterContext context)
        {
            if (context.Set<Profile>().Count() == 0)
                context.Set<Profile>().Add(new Profile()
                {
                    Name = "Administrator",
                    Description = "",
                    Roles = new List<Role>() { new Role() { Name = "user" } }
                });

            if (context.Set<User>().Count() == 0)
                context.Set<User>().Add(new User()
                {
                    Name = "Billie Kim",
                    Password = MD5.Encrypt("wildbill"),
                    Username = "bbkim",
                    Email = "billie.kim46@example.com",
                    ProfileId = 1
                });

            context.SaveChanges();
        }
    }
}
