using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hangman.Service
{
    public interface IUserService<T> where T : class
    {
        Task<IdentityResult> CreateAsync(T userIdentity, string password);

        Task<IdentityResult> AddToRoleAsync(T userIdentity, string roleName);

        Task<T> FindByIdAsync(string userId);

        Task<T> FindByNameAsync(string userName);

        Task<bool> CheckPasswordAsync(T user, string password);

        Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password);

        Task<IdentityResult> AddClaimAsync(T userIdentity, string roleName);

        Task<IList<Claim>> GetClaimsAsync(T userIdentity);
    }
}