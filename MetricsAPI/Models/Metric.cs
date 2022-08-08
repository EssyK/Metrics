using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetricsAPI.Models
{
    public class Metric
    {
        public Metric()
        {
            TimeStamp = DateTime.UtcNow;
        }

        [Key]
        public long Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime TimeStamp { get; set; }
        public long Value { get; set; }

        public int MetricId { get; set; }

        [ForeignKey("MetricId")]
        public virtual MetricDefinition MetricDefinition { get; set; }
    }
}
