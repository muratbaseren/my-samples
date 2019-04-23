using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication26
{
    public class ProductPicture
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(80), Required]
        public string FileName { get; set; }

        public int ProductId { get; set; }


        public virtual Product Product { get; set; }

        public ProductPicture()
        {
            Id = Guid.NewGuid();
        }
    }
}