using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace sleep_tracker_api.Models
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Record> Records { get; set; }
        public ApplicationDbContext() : base() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .; Database = SleepTrackerDB; TrustServerCertificate = True; Trusted_Connection = True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>().Property(r => r.Duration)
                .HasConversion(
                    duration => duration.ToString(),
                    durationStr => TimeSpan.Parse(durationStr)
                );
            base.OnModelCreating(modelBuilder);
        }

    }
}
