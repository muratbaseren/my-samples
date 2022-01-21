using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppWithEFCore
{
    public partial class Album
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public int? Duration { get; set; }
        [StringLength(50)]
        public string Author { get; set; }
    }
}
