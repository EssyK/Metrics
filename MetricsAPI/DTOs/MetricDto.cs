using MetricsAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MetricsAPI.DTOs
{
    public class MetricDefinitionDto : BaseDto<MetricDefinitionDto, MetricDefinition>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class MetricDto : BaseDto<MetricDto, Metric>
    {
        [Required]
        public int MetricId { get; set; }

        [Required]
        public long Value { get; set; }
    }

    public class MetricDisplayDto : BaseDto<MetricDisplayDto, Metric>
    {
        public string Name { get; set; }
        public int MetricId { get; set; }
        public long Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public string MonthDay { get; set; }
        public int MonthNumber { get; set; }
        public int Year { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomEntityDtoMapping()
                .Map(dest => dest.Name, src => src.MetricDefinition.Name)
                .Map(dest => dest.MonthDay, src => src.TimeStamp.ToString("MMM d"))
                .Map(dest => dest.MonthNumber, src => int.Parse(src.TimeStamp.ToString("MM")))
                .Map(dest => dest.Year, src => int.Parse(src.TimeStamp.ToString("yyyy")));
        }
    }

    public class MetricValueDto
    {
        public string Name { get; set; }
        public long Value { get; set; }
        public string MonthDay { get; set; }
        public string MonthString { get; set; }
        public int Month { get; set; }
    }

}
