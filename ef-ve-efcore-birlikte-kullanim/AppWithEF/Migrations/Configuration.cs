namespace AppWithEF.Migrations
{
    using FakeData;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AppWithEF.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppWithEF.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (!context.Albums.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    context.Albums.Add(new Album
                    {
                        Name = NameData.GetCompanyName(),
                        Description = TextData.GetSentence(),
                        Duration = NumberData.GetNumber(),
                        Author = NameData.GetFullName()
                    });
                }

                context.SaveChanges();
            }
            else
            {
                context.Albums.ToList().ForEach(x =>
                {
                    if (x.Duration == null || x.Duration == 0)
                    {
                        x.Duration = NumberData.GetNumber(500, 1000);
                    }

                    if (string.IsNullOrEmpty(x.Author))
                    {
                        x.Author = NameData.GetFullName();
                    }
                });

                if (context.ChangeTracker.HasChanges())
                    context.SaveChanges();
            }
        }
    }
}
