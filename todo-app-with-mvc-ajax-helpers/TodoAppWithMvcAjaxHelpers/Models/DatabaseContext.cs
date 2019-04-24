using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace TodoAppWithMvcAjaxHelpers
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
    }

    public class Todo
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(30)]
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
    }
}