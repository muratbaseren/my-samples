namespace WebApplication22.Migrations
{
    using System.Data.Entity.Migrations;
    using WebApplication22.Context;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseContext context)
        {
            DatabaseInitializer.InsertSeedData(context);
        }
    }
}
