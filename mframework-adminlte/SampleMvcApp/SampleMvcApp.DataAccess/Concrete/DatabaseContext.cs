using SampleMvcApp.Entities.Concrete;
using System.Data.Entity;

namespace SampleMvcApp.DataAccess.Concrete
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new DatabaseContextInitializer());
        }
    }

    public class DatabaseContextInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            // Members..
            for (int i = 0; i < 15; i++)
            {
                context.Members.Add(new Member
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    IsActive = FakeData.BooleanData.GetBoolean(),
                    Password = MlkPwgen.PasswordGenerator.Generate(6)                    
                });
            }

            context.SaveChanges();

            // Customers..
            for (int i = 0; i < 10; i++)
            {
                context.Customers.Add(new Customer
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname()
                });
            }

            context.SaveChanges();

            // Departments..
            for (int i = 0; i < 5; i++)
            {
                context.Departments.Add(new Department
                {
                    Name = FakeData.NameData.GetCompanyName(),
                    Description = FakeData.TextData.GetSentence()
                });
            }

            context.SaveChanges();
        }
    }
}
