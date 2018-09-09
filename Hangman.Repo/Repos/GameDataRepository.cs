using Hangman.Domain;

namespace Hangman.Repo
{
    public class GameDataRepository : GenericRepository<GameData>
    {
        public GameDataRepository(ApplicationDbContext appContext) : base(appContext)
        {
        }
    }
}
