using System.ComponentModel.DataAnnotations;

namespace WebApiToken.DataTransferObjects
{
    public class UserForRegistrationDto
    {
        [Required, StringLength(16)]
        public string Username { get; set; }

        [Required, StringLength(8), MinLength(4)]
        public string Password { get; set; }
    }
}
