using System.Data.Entity.Migrations;

namespace DemoLab.Data.Access.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context.ApplicationDbContext>
    {
        public Configuration()
        {
            ContextKey = "ApplicationData";
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Context.ApplicationDbContext context)
        {
        }
    }
}
