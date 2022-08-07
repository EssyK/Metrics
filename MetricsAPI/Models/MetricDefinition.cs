using System.ComponentModel.DataAnnotations;

namespace MetricsAPI.Models
{
    public class MetricDefinition
    {
        public MetricDefinition()
        {
            Metrics = new List<Metric>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Metric> Metrics { get; set; }
    }
}
