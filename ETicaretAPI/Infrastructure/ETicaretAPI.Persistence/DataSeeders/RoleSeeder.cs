using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.DataSeeders
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<InvalidProgramException>>(); // Program veya başka bir sınıf kullanın
            logger.LogInformation("Role seeding started.");

            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
                logger.LogInformation("RoleManager service resolved successfully.");

                string[] roleNames = { "Admin", "Authorized", "SupplierManager", "Employee", "EmployeeRead", "Customer" };

                foreach (var roleName in roleNames)
                {
                    logger.LogInformation("Checking if role {RoleName} exists...", roleName);
                    var roleExists = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExists)
                    {
                        logger.LogInformation("Role {RoleName} does not exist. Creating...", roleName);
                        var result = await roleManager.CreateAsync(new AppRole { Name = roleName });
                        if (result.Succeeded)
                        {
                            logger.LogInformation("Role {RoleName} created successfully.", roleName);
                        }
                        else
                        {
                            logger.LogError("Failed to create role {RoleName}. Errors: {Errors}", roleName, string.Join(", ", result.Errors.Select(e => e.Description)));
                        }
                    }
                    else
                    {
                        logger.LogInformation("Role {RoleName} already exists.", roleName);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding roles.");
                throw;
            }
        }
    }
}
