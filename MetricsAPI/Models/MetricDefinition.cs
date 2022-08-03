using System.ComponentModel.DataAnnotations;

namespace MetricsAPI.Models
{
    public class MetricDefinition
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Metric> Metrics { get; set; }
    }
}
