using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Prng.Drbg;
using ProductsManagement.Application.Enums;
using ProductsManagement.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Infrastructure.Identity.Seeds
{
    public static class DefaultAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                FirstName = "Goda",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "MySuperP@$$w0rd!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }

            }
        }
    }
}
