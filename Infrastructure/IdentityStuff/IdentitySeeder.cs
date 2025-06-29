using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IdentityStuff.IdentitySeeder
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await EnsureRolesExist(roleManager, new[] { "Admin", "User" });

            var adminEmail = "admin@admin.com";
            var adminPassword = "@dm1NNN";

            var adminUser = await GetOrCreateAdminUser(userManager, adminEmail, adminPassword);

            await EnsureUserInRole(userManager, adminUser, "Admin");
        }

        private static async Task EnsureRolesExist(RoleManager<IdentityRole> roleManager, string[] roles)
        {
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role)) await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private static async Task<IdentityUser> GetOrCreateAdminUser(UserManager<IdentityUser> userManager, string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null) return user;

            user = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new InvalidDataException("Unable to create administrator: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            return user;
        }

        private static async Task EnsureUserInRole(UserManager<IdentityUser> userManager, IdentityUser user, string role)
        {
            if (!await userManager.IsInRoleAsync(user, role)) await userManager.AddToRoleAsync(user, role);
        }
    }
}
