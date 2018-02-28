
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PAWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAWeb.Handler
{
    public class UserHandler
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;

            roleManager.CreateAsync(new IdentityRole { Name = "Admin" }).Wait();
        }

        async public Task CreateAdmin()
        {
            var applicationUser = new ApplicationUser
            {
                UserName = "anderssonpascal@gmail.com",
                Password = "teparty"
            };

            await userManager.CreateAsync(applicationUser);
            await userManager.AddToRoleAsync(applicationUser, "Admin");
        }

        async public Task<bool> TrySignInAdmin(string userName, string password)
        {
            var userFromDb = await userManager.FindByNameAsync(userName);

            if (userFromDb.Password == password)
            {
                await signInManager.SignInAsync(userFromDb, true);

                await userManager.UpdateAsync(userFromDb);

                return userFromDb.IsSignedIn = true;
            }
            else
                return userFromDb.IsSignedIn = false;
        }

        async public Task SignOutAdmin(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            await signInManager.SignOutAsync();
            user.IsSignedIn = false;
            await userManager.UpdateAsync(user);
        }
    }
}
