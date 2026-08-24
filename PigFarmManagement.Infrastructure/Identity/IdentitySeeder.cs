using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Infrastructure.Data;

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
            IConfiguration configuration,
            PigFarmDbContext db)
        {
            var adminEmail = (configuration["DevelopmentSeed:AdminEmail"]
                ?? "admin@pigfarm.local").Trim();

            var adminPassword = configuration["DevelopmentSeed:AdminPassword"]
                ?? "Password@123";


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

                var createResult = await userManager.CreateAsync(
                    admin,
                    adminPassword);

                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(
                        admin,
                        AppRoles.Admin);
                }

                // Ensure a farm exists and assign it to the admin
                var farm = db.Farms.FirstOrDefault();
                if (farm == null)
                {
                    farm = new Farm
                    {
                        Id = Guid.NewGuid(),
                        Name = configuration["DevelopmentSeed:FarmName"] ?? "Demo Farm",
                        FarmCode = configuration["DevelopmentSeed:FarmCode"] ?? "DF-001",
                        Location = configuration["DevelopmentSeed:FarmLocation"] ?? "Local",
                        Currency = configuration["DevelopmentSeed:FarmCurrency"] ?? "USD",
                        TimeZone = configuration["DevelopmentSeed:FarmTimeZone"] ?? "UTC",
                        LastAnimalSequence = 0,
                        LastBuildingSequence = 0
                    };

                    db.Farms.Add(farm);
                    await db.SaveChangesAsync();
                }

                admin.FarmId = farm.Id;
                await userManager.UpdateAsync(admin);

                return;
            }


            // User already exists
            // Do not reset password here

            // User already exists
            // Do not reset password here

            if (!await userManager.IsInRoleAsync(admin, AppRoles.Admin))
            {
                await userManager.AddToRoleAsync(
                    admin,
                    AppRoles.Admin);
            }

            // Ensure the existing user has a farm assigned
            if (!admin.FarmId.HasValue)
            {
                var farm = db.Farms.FirstOrDefault();
                if (farm == null)
                {
                    farm = new Farm
                    {
                        Id = Guid.NewGuid(),
                        Name = configuration["DevelopmentSeed:FarmName"] ?? "Demo Farm",
                        FarmCode = configuration["DevelopmentSeed:FarmCode"] ?? "DF-001",
                        Location = configuration["DevelopmentSeed:FarmLocation"] ?? "Local",
                        Currency = configuration["DevelopmentSeed:FarmCurrency"] ?? "USD",
                        TimeZone = configuration["DevelopmentSeed:FarmTimeZone"] ?? "UTC",
                        LastAnimalSequence = 0,
                        LastBuildingSequence = 0
                    };

                    db.Farms.Add(farm);
                    await db.SaveChangesAsync();
                }

                admin.FarmId = farm.Id;
                await userManager.UpdateAsync(admin);
            }

            // Ensure a development farm exists and assign it to any users missing FarmId
            var devFarm = db.Farms.FirstOrDefault();
            if (devFarm == null)
            {
                devFarm = new Farm
                {
                    Id = Guid.NewGuid(),
                    Name = configuration["DevelopmentSeed:FarmName"] ?? "Demo Farm",
                    FarmCode = configuration["DevelopmentSeed:FarmCode"] ?? "DF-001",
                    Location = configuration["DevelopmentSeed:FarmLocation"] ?? "Local",
                    Currency = configuration["DevelopmentSeed:FarmCurrency"] ?? "USD",
                    TimeZone = configuration["DevelopmentSeed:FarmTimeZone"] ?? "UTC",
                    LastAnimalSequence = 0,
                    LastBuildingSequence = 0
                };

                db.Farms.Add(devFarm);
                await db.SaveChangesAsync();
            }

            var usersWithoutFarm = userManager.Users.Where(u => !u.FarmId.HasValue).ToList();
            foreach (var u in usersWithoutFarm)
            {
                u.FarmId = devFarm.Id;
                await userManager.UpdateAsync(u);
            }
        }
    }
}
