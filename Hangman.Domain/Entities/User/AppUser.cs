using Microsoft.AspNetCore.Identity;

namespace Hangman.Domain
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
    }
}
