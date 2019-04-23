using WebApplication22.Models;
using System.Data.Entity;

namespace WebApplication22.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new DatabaseInitializer());
        }
    }
}
