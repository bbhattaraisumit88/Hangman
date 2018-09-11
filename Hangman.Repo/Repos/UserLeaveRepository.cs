using Hangman.Domain;

namespace Hangman.Repo
{
    public class UserLeaveRepository : GenericRepository<UserLeave>
    {
        public UserLeaveRepository(ApplicationDbContext appContext) : base(appContext)
        {
        }
    }
}
