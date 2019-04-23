
namespace EFCodeGenerator
{
    using System;

    public static class StringExtensions
    {
        public static string NewLine(this string s)
        {
            //return s + Environment.NewLine;
            return s + "<br />";
        }

        public static string NewLine(this string s, int count)
        {
            for (int i = 0; i < count; i++)
                s = s.NewLine();

            return s;
        }

        public static string ApplyPadding(this string s)
        {
            for (int i = 1; i <= 5; i++)
            {
                s = s.Replace($"[p{i}]", $"<span style='padding-left:{i*15}px;'></span>");
            }

            return s;
        }

        public static string ApplyBreakingLine(this string s)
        {
            return s.Replace("[br]", "<br />");
        }
    }
}


namespace EFCodeGenerator.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    [Table("Students", Schema ="aa")]
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "xxx"), DisplayName("Name")]
        public string Name { get; set; }

        public virtual List<string> Messages { get; set; }
    }

    public class DatabaseContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
    }
}