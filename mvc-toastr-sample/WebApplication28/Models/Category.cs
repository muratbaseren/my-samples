using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication28.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50), DisplayName("Category Name")]
        public string Name { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public virtual List<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }
    }
}
