using System.ComponentModel.DataAnnotations;

namespace Hangman.Domain
{
    public class RegistrationDTO
    {
        [Required(ErrorMessage = "FirstName cannot be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName cannot be empty")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Contact number cannot be empty")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address cannot be empty")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email cannot be empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName cannot be empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password cannot be empty")]
        [Compare("Password", ErrorMessage = "Password and Confirm password do not match")]
        public string ConFirmPassword { get; set; }

        [Required(ErrorMessage = "Profile picture is mandatory")]
        public string ImageUrl { get; set; }
    }
}
