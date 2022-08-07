using Microsoft.EntityFrameworkCore;
using MetricsAPI.Models;

namespace MetricsAPI.DataLayer
{
    public class MetricsDbContext : DbContext
    {
        public MetricsDbContext(DbContextOptions<MetricsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Metric>()
                .HasOne(p => p.MetricDefinition)
                .WithMany(m => m.Metrics)
                .HasForeignKey(m => m.MetricId);
        }

        public DbSet<Metric> Metrics { get; set; }
        public DbSet<MetricDefinition> MetricDefinitions { get; set; }
    }
}
