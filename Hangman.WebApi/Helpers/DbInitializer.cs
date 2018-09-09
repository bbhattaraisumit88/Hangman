using Hangman.Domain;
using Hangman.Service.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hangman.Web.Helpers
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            try
            {
                context.Database.EnsureCreated();
                string role = Constants.Strings.DefaultRoles.Superuser;
                string guestRole = Constants.Strings.DefaultRoles.Guest;
                RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roleNames = { role, guestRole };

                IdentityResult roleResult;

                foreach (string roleName in roleNames)
                {
                    bool roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
                var config = new ConfigurationBuilder()

                .SetBasePath(Directory.GetCurrentDirectory())

                .AddJsonFile("appsettings.json")

                .Build();

                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

                var user = await userManager.FindByNameAsync(config.GetSection("AppSettings")["UserName"]);

                if (user == null)
                {
                    var superuser = new AppUser
                    {
                        UserName = config.GetSection("AppSettings")["UserName"]
                    };

                    string userPassword = config.GetSection("AppSettings")["UserPassword"];

                    var createSuperUser = await userManager.CreateAsync(superuser, userPassword);                   
                    if (createSuperUser.Succeeded)
                    {
                        await userManager.AddClaimAsync(superuser, new Claim(ClaimTypes.Role, role));
                        await userManager.AddToRoleAsync(superuser, role);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}