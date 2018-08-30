using Microsoft.AspNetCore.Identity;

namespace Hangman.Domain
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
