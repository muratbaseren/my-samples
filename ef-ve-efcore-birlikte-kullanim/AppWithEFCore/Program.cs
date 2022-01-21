using System;
using System.Linq;

namespace AppWithEFCore
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
}
