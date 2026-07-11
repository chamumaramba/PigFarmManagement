using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PigFarmManagement.Infrastructure.Identity;

namespace PigFarmManagement.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedIdentityAsync(this IApplicationBuilder app)
        {
           using var scope = app.ApplicationServices.CreateScope();
           var roleManager =
            scope.ServiceProvider
            .GetRequiredService<RoleManager<ApplicationRole>>();

            var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

            await IdentitySeeder.SeedRoleAsync(userManager, roleManager);
            await IdentitySeeder.SeedAdminAsync(userManager);

        }
    }
}