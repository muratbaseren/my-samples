using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication9
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            
            DatabaseContext db = new DatabaseContext();

            try
            {
                db.Kisiler.ToList();
            }
            catch (Exception)
            {
                db.Database.ExecuteSqlCommand(@"
                            CREATE TABLE Kisis 
                            (Id int, Name nvarchar(30));
                            INSERT INTO Kisis (Id, Name)
                            VALUES (1, 20);
                            INSERT INTO Kisis (Id, Name)
                            VALUES (2, 30);
                            INSERT INTO Kisis (Id, Name)
                            VALUES (3, 50);");
            }

            var list = db.Kisiler.ToList();
            var count = list.Count;

            db.Kisiler.Add(new Kisi() { Id = count+1, Name = "Murat" });
            db.SaveChanges();

            return View(db.Kisiler.ToList());
        }
    }

    public class DatabaseContext : DbContext
    {
        public DbSet<Kisi> Kisiler { get; set; }
    }

    public class Kisi
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}