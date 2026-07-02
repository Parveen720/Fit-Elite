using Fit_Elite.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Fit_Elite.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAdminAsync(ApplicationDbContext context, IConfiguration config)
        {
            var roles = new[] { "Admin", "Member", "GymOwner" };

            foreach (var roleName in roles)
            {
                if (!await context.Roles.IgnoreQueryFilters().AnyAsync(r => r.Name == roleName))
                {
                    context.Roles.Add(new Role
                    {
                        Name = roleName
                    });
                }
            }

            await context.SaveChangesAsync();

            var adminRole = await context.Roles
                .IgnoreQueryFilters()
                .FirstAsync(r => r.Name == "Admin");

            if (adminRole == null)
            {
                adminRole = new Role { Name = "Admin" };
                context.Roles.Add(adminRole);
                await context.SaveChangesAsync();
            }


            bool adminExists = await context.Users
                .IgnoreQueryFilters()
                .AnyAsync(u => u.UserRole != null && u.UserRole.RoleId == adminRole.Id);

            if (adminExists) return;

           
            var adminEmail = config["AdminSeed:Email"];
            var adminPassword = config["AdminSeed:Password"];
            var adminName = config["AdminSeed:FullName"] ?? "System Admin";

            if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
            {

                return;
            }

            var adminUser = new User
            {
                FullName = adminName,
                Email = adminEmail.Trim().ToLower(),
                PasswordHash = BCryptNet.HashPassword(adminPassword)
            };

            adminUser.UserRole = new UserRole
            {
                User = adminUser,
                RoleId = adminRole.Id
            };

            context.Users.Add(adminUser);
            await context.SaveChangesAsync();
        }
    }
}