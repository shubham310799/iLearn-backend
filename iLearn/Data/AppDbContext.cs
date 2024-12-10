using iLearn.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace iLearn.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            var roles = new List<Role>()
            {
                new Role()
                {
                    Id = new Guid("3bfefb41-6944-4b9f-a71c-9f668533a51b"),
                    RoleName = "User",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 12, 08, 00, 00, 00)
                },
                new Role()
                {
                    Id = new Guid("158ba883-59ee-4fb8-ab26-80d515187a81"),
                    RoleName = "Instructor",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 12, 08, 00, 00, 00)
                }
            };

            modelBuilder.Entity<Role>().HasData(roles);
        }
    }
}
