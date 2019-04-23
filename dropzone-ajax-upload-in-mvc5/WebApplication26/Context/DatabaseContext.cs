using System.Data.Entity;

namespace WebApplication26
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }

        public DatabaseContext()
        {
            //Database.SetInitializer(new DatabaseInitializer());
        }
    }
}
