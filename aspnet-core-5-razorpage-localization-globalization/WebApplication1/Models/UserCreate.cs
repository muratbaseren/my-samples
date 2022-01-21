using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UserCreate
    {
        [Required(ErrorMessage ="Name is required.")]
        public string Name { get; set; }
    }
}
