using Microsoft.EntityFrameworkCore;
using WebApiToken.Models;

namespace WebApiToken.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
               
        }

        public DbSet<User> Users { get; set; }
    }

    public class DataContextInitializer
    {
        public void Seed(DatabaseContext context)
        {
            //context.Users.Add(new User()
            //{
                
            //});
        }
    }
}
