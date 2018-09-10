using Hangman.Domain;

namespace Hangman.Repo
{
    public class UserRepository : GenericRepository<AppUser>
    {
        public UserRepository(ApplicationDbContext appContext) : base(appContext)
        {
        }
    }
}
