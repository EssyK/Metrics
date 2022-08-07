namespace MetricsAPI.DTOs
{
    public class MetricAveragesDto
    {
        public List<MetricValueDto> PerMinute { get; set; }
        public List<MetricValueDto> PerHour { get; set; }
        public List<MetricValueDto> PerDay { get; set; }
    }
}
