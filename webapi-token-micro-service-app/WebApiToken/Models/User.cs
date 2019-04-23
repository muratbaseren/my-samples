using System.ComponentModel.DataAnnotations;

namespace WebApiToken.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(16)]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required, StringLength(16)]
        public string AppName { get; set; }
    }
}