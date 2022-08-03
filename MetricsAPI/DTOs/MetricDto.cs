using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MetricsAPI.DTOs
{
    public class MetricDefinitionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MetricDto
    {
        public int MetricId { get; set; }
        public long Value { get; set; }
    }

    public class MetricDisplayDto
    {
        public string Name { get; set; }
        public int MetricId { get; set; }
        public long Value { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class MetricDtoResponse<T>
    {        
        public T Value { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
