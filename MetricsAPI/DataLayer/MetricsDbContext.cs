using Microsoft.EntityFrameworkCore;
using MetricsAPI.Models;

namespace MetricsAPI.DataLayer
{
    public class MetricsDbContext : DbContext
    {
        public MetricsDbContext(DbContextOptions<MetricsDbContext> options) : base(options)
        {
        }

        public DbSet<Metric> Metrics { get; set; }
        public DbSet<MetricDefinition> MetricDefinitions { get; set; }
    }
}
