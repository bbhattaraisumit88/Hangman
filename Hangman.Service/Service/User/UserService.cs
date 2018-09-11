using Hangman.Domain;
using Hangman.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hangman.Service
{
    public class UserService<T> : IUserService<T> where T : AppUser
    {
        private readonly UserManager<T> _userManager;
        private readonly IJwtService _jwtService;
        private readonly ApplicationDbContext appContext;
        private readonly IUnitOfWork _uow;

        public UserService(UserManager<T> userManager, ApplicationDbContext appContext, IJwtService jwtService, IUnitOfWork _uow)
        {
            this._userManager = userManager;
            this.appContext = appContext;
            this._jwtService = jwtService;
            this._uow = _uow;
        }

        public IQueryable<object> GetAllAsync()
        {
            try
            {
                return from users in appContext.Users
                       join userroles in appContext.UserRoles on users.Id equals userroles.UserId
                       join roles in appContext.Roles on userroles.RoleId equals roles.Id
                       where users.UserName != "superuser"
                       select new { users.Id, users.UserName, users.FirstName, users.LastName, users.Address, users.PhoneNumber, RoleId = roles.Id, roles.Name, users.ImageUrl };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<object> SearchAllAsyncByName(string userName)
        {
            try
            {
                return from users in appContext.Users
                       join userroles in appContext.UserRoles on users.Id equals userroles.UserId
                       join roles in appContext.Roles on userroles.RoleId equals roles.Id
                       where users.UserName != "superuser" && users.UserName.Contains(userName)
                       select new { users.Id, users.UserName, users.FirstName, users.LastName, users.Address, users.PhoneNumber, RoleId = roles.Id, roles.Name, users.ImageUrl };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<object> GetAllRoles()
        {
            try
            {
                return appContext.Roles.Select(x => new { value = x.Id, label = x.Name });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IdentityResult> CreateAsync(T userIdentity, string password)
        {
            try
            {
                var result = await _userManager.CreateAsync(userIdentity, password);
                if (result.Succeeded) await appContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IdentityResult> DeleteAsync(T userIdentity)
        {
            try
            {
                var result = await _userManager.DeleteAsync(userIdentity);
                if (result.Succeeded)
                {
                    await _userManager.RemoveFromRoleAsync(userIdentity, "guest");
                    await appContext.SaveChangesAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IdentityResult> AddToRoleAsync(T userIdentity, string roleName)
        {
            try
            {
                var result = await _userManager.AddToRoleAsync(userIdentity, roleName);
                if (result.Succeeded) await appContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<T> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> CheckPasswordAsync(T user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                    return await Task.FromResult<ClaimsIdentity>(null);

                // get the user to verifty
                var userToVerify = await FindByNameAsync(userName);

                if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

                var userClaims = await GetClaimsAsync(userToVerify);
                // check the credentials
                if (await CheckPasswordAsync(userToVerify, password))
                {
                    return await Task.FromResult(_jwtService.GenerateClaimsIdentity(userName, userToVerify.Id.ToString(), userClaims.SingleOrDefault().Value));
                }

                // Credentials are invalid, or account doesn't exist
                return await Task.FromResult<ClaimsIdentity>(null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IdentityResult> AddClaimAsync(T userIdentity, string roleName)
        {
            return await _userManager.AddClaimAsync(userIdentity, new Claim(ClaimTypes.Role, roleName));
        }

        public async Task<IList<Claim>> GetClaimsAsync(T userIdentity)
        {
            return await _userManager.GetClaimsAsync(userIdentity);
        }
    }
}
