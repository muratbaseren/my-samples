using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicallyCreateTableWİthEF
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseContext db = new DatabaseContext();
            db.Employees.ToList();
            db.Dispose();


            DatabaseContext db2 = new DatabaseContext();
            db2.Employees.ToList();
            db2.Dispose();

            Console.ReadKey();
        }
    }

    public class DatabaseContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }

    public class Employee : EntityBase
    {
        [StringLength(30), Required]
        public string Name { get; set; }

        [StringLength(30), Required]
        public string Surname { get; set; }
    }

}
