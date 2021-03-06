using System.Data.Entity.Migrations;
using ForumETF.Models;

namespace ForumETF.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "ForumETF.Models.DbContext";
        }

        protected override void Seed(AppDbContext context)
        {
            /*
            context.Tags.AddOrUpdate(t => t.TagName,
                new Tag { TagName = "Programiranje" },
                new Tag { TagName = "C#" },
                new Tag { TagName = "Prosti brojevi" },
                new Tag { TagName = "Permutacije" }
                );
            */

            //context.Categories.AddOrUpdate(c => c.CategoryName,
            //    new Category { CategoryName = "Matematika" },
            //    new Category { CategoryName = "Fizika" },
            //    new Category { CategoryName = "Osnovi elektrotehnike 1" },
            //    new Category { CategoryName = "Programski jezici" }
            //    );

            //context.Roles.AddOrUpdate(r => r.Name,
            //    new IdentityRole { Name = "Admin"}
            //    );


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
