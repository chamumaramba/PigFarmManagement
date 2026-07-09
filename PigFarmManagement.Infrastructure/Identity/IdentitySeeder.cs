using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PigFarmManagement.Infrastructure.Identity
{
    public static class IdentitySeeder
    {
        public static async Task SeedRoleAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            // Seed roles
            var roles = new[] { "Admin", "FarmManger", "Veterinarian", "FarmWorker" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole(role));
                }
            }

        }    // Seed admin user
        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var adminEmail = "admin@pigfarm.com";

            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User"
                };

                var result = await userManager.CreateAsync(admin, "Password@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}