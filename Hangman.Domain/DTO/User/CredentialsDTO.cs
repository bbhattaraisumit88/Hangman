using System.ComponentModel.DataAnnotations;

namespace Hangman.Domain
{
    public class CredentialsDTO
    {
        [Required(ErrorMessage = "Username cannot be empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [StringLength(12, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
