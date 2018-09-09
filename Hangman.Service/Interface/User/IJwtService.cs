using System.Security.Claims;
using System.Threading.Tasks;

namespace Hangman.Service
{
    public interface IJwtService
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);

        ClaimsIdentity GenerateClaimsIdentity(string userName, string id, string claimValue);
    }
}