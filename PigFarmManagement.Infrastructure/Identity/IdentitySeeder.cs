using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace PigFarmManagement.Infrastructure.Identity
{
    public static class IdentitySeeder
    {
        public static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager)
        {
            var roles = new[] { AppRoles.Admin, AppRoles.FarmManager, AppRoles.Veterinarian, AppRoles.FarmWorker };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole(role));
                }
            }

        }

        public static async Task SeedDevelopmentAdminAsync(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            var adminEmail = (configuration["DevelopmentSeed:AdminEmail"] ?? "admin@pigfarm.local").Trim();
            var adminPassword = configuration["DevelopmentSeed:AdminPassword"] ?? "Password@123";

            if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
            {
                return;
            }

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

                var createResult = await userManager.CreateAsync(admin, adminPassword);
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, AppRoles.Admin);
                }

                return;
            }

            admin.UserName = adminEmail;
            admin.Email = adminEmail;
            admin.EmailConfirmed = true;
            admin.FirstName = "Admin";
            admin.LastName = "User";

            await userManager.UpdateAsync(admin);

            var token = await userManager.GeneratePasswordResetTokenAsync(admin);
            var passwordResult = await userManager.ResetPasswordAsync(admin, token, adminPassword);
            if (passwordResult.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, AppRoles.Admin);
            }
        }
    }
}
