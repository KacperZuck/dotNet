using Health_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Health_App.Data
{
    public class ConfigDbContext : DbContext
    {
        public ConfigDbContext(DbContextOptions<ConfigDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> user { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    id = Guid.NewGuid(),
                    name = "Admin",
                    surname = "Admin",
                    email = "admin@health.pl",
                    pasword = "admin123",
                    birth_date = new DateOnly(1990, 1, 1)
                },
                new User
                {
                    id = Guid.NewGuid(),
                    name = "Test",
                    surname = "Test",
                    email = "test@test.pl",
                    pasword = "test123",
                    birth_date = new DateOnly(2000, 5, 15)
                }
            );
        }
    }
}