using System.ComponentModel.DataAnnotations;

namespace WebApplication22.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
    }
}