using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AppWithEF
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseContext databaseContext = new DatabaseContext();
            var albums = databaseContext.Albums.ToList();

            albums.ForEach(x => Console.WriteLine(x.Name + " | " + x.Author));

            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }

    public class DatabaseContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
    }

    [Table("Albums")]
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(25)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Author { get; set; }
        public int? Duration { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
    }
}
