using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Tracking.DataAccessLayer
{
    public class DataContext : DbContext
    {
        public DbSet<TrackingData> TrackingData { get; set; }
        public DbSet<User> User { get; set; }

        public DataContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DbContext"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrackingData>()
                .HasOne<User>(t => t.User)
                .WithMany(u => u.Tracking)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .HasIndex(td => new { td.FirstName, td.LastName }).IsUnique();
        }
    }
}