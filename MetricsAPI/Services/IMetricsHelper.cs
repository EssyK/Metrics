using MetricsAPI.DTOs;
using MetricsAPI.Models;

namespace MetricsAPI.Services
{
    public interface IMetricsHelper
    {
        public Task<MetricAveragesDto> GetMetricsAverages(List<Metric> metrics);
    }
}
