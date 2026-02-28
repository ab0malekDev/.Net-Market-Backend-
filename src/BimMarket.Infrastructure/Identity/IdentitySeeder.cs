using Microsoft.AspNetCore.Identity;

namespace BimMarket.Infrastructure.Identity;

public static class IdentitySeeder
{
    public static async Task SeedAdminAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        const string adminRoleName = "Admin";
        const string adminEmail = "admin@bimmarket.com";
        const string adminPassword = "Admin123!";

        // Ensure role exists
        if (!await roleManager.RoleExistsAsync(adminRoleName))
        {
            var role = new ApplicationRole
            {
                Name = adminRoleName,
                NormalizedName = adminRoleName.ToUpperInvariant(),
                Description = "Default administrator role",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await roleManager.CreateAsync(role);
        }

        // Ensure admin user exists
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                NormalizedUserName = adminEmail.ToUpperInvariant(),
                Email = adminEmail,
                NormalizedEmail = adminEmail.ToUpperInvariant(),
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (!result.Succeeded)
            {
                // If creation fails (e.g. password policy), just return and let caller decide what to do.
                return;
            }
        }

        // Ensure user is in Admin role
        if (!await userManager.IsInRoleAsync(adminUser, adminRoleName))
        {
            await userManager.AddToRoleAsync(adminUser, adminRoleName);
        }
    }
}

