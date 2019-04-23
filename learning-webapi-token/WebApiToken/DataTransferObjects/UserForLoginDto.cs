using System.ComponentModel.DataAnnotations;

namespace WebApiToken.DataTransferObjects
{
    public class UserForLoginDto
    {
        [Required, StringLength(16)]
        public string Username { get; set; }

        [Required, StringLength(8), MinLength(4)]
        public string Password { get; set; }
    }
}
