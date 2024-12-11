using LearningPlatformBackend.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LearningPlatformBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
