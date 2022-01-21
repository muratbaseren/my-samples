using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEMO.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60)]
        public string NameSurname { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        public bool IsBlocked { get; set; }

        public virtual List<Expense> Expenses { get; set; }
    }

    [Table("Expenses")]
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        
        [StringLength(160)]
        public string Description { get; set; }
        public decimal Price { get; set; }

        [Required]
        [StringLength(40)]
        public string Category { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
