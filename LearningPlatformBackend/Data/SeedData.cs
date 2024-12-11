using LearningPlatformBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformBackend.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            if (await context.Roles.AnyAsync()) return; // Avoid reseeding if already seeded.

            var roles = new List<Role>
            {
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            };

            await context.Roles.AddRangeAsync(roles);

            await context.Users.AddAsync(new User
            {
                Id = 1,
                Username = "admin",
                Email = "almaskhan@yopmail.com",
                PasswordHash = "adminpassword", // Use hashed passwords in production!
                RoleId = 1
            });

            await context.SaveChangesAsync();
        }
    }
}
