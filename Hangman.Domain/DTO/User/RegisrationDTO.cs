using System.ComponentModel.DataAnnotations;

namespace Hangman.Domain
{
    public class RegistrationDTO
    {
        [Required(ErrorMessage = "UserName cannot be empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email cannot be empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }

        [Required(ErrorMessage = "FirstName cannot be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName cannot be empty")]
        public string LastName { get; set; }
    }
}
