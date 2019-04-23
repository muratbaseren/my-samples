using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EndlessMenuSampleMvc
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ActionName { get; set; }      // Index gibi.
        public string ControllerName { get; set; }  // Home gibi.
        public string QueryStrings { get; set; }    // "?lang=en&search=abc" gibi.
        public int OrderNo { get; set; }
        public int? ParentCategoryId { get; set; }

        public virtual Category ParentCategory { get; set; }
        public virtual List<Category> Categories { get; set; }
    }

    public class EndlessMenuContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public EndlessMenuContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
    }

    public class DbInitializer : CreateDatabaseIfNotExists<EndlessMenuContext>
    {
        // Örnek veri oluşturma işlemini yapan kodlar..
        // Veritabanı oluşumu sonrasında veritabanına eklenecektir.

        protected override void Seed(EndlessMenuContext context)
        {
            // Root category'ler insert edilir.
            for (int i = 0; i < FakeData.NumberData.GetNumber(4, 8); i++)
            {
                Category rootcat = new Category
                {
                    Name = $"Root Menu {i}",
                    ParentCategory = null,
                    OrderNo = i,
                    ControllerName = "Home",
                    ActionName = "Index",
                    Description = FakeData.TextData.GetSentence()
                };

                context.Categories.Add(rootcat);
            }

            context.SaveChanges();

            // Root category'ler altındaki sub category'ler insert edilir.
            foreach (Category rootcat in context.Categories.Where(x => x.ParentCategory == null).ToList())
            {
                for (int i = 0; i < FakeData.NumberData.GetNumber(4, 8); i++)
                {
                    Category sub1cat = new Category
                    {
                        Name = $"Sub Menu {i}",
                        ParentCategory = rootcat,
                        OrderNo = i,
                        ControllerName = "Home",
                        ActionName = "Index",
                        Description = FakeData.TextData.GetSentence()
                    };

                    context.Categories.Add(sub1cat);
                }
            }

            context.SaveChanges();

            // Sub category'ler altındaki sub sub category'ler insert edilir.
            foreach (Category subcat in context.Categories.Where(x => x.ParentCategory != null).ToList())
            {
                for (int i = 0; i < FakeData.NumberData.GetNumber(2, 5); i++)
                {
                    Category sub2cat = new Category
                    {
                        Name = $"Sub Sub Menu {i}",
                        ParentCategory = subcat,
                        OrderNo = i,
                        ControllerName = "Home",
                        ActionName = "Index",
                        Description = FakeData.TextData.GetSentence()
                    };

                    context.Categories.Add(sub2cat);
                }
            }

            context.SaveChanges();
        }
    }
}