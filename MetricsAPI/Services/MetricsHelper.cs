using Mapster;
using MetricsAPI.DTOs;
using MetricsAPI.Models;

namespace MetricsAPI.Services
{
    public class MetricsHelper : IMetricsHelper
    {
        const short hoursPerDay = 24;
        const short minutesPerDay = 1440;

        public async Task<MetricAveragesDto> GetMetricsAverages(List<Metric> metrics)
        {
            try
            {
                MetricAveragesDto metricAveragesDto = new MetricAveragesDto();

                if (metrics.Count > 0)
                {
                    var metricsDto = metrics.Adapt<List<MetricDisplayDto>>();

                    var avgDay = metricsDto.GroupBy(x => new { x.MetricId, x.MonthNumber }).Select(g => new
                    MetricValueDto
                    {
                        Value = g.Sum(s => s.Value)/(DateTime.DaysInMonth(g.First().Year, g.First().MonthNumber)),
                        Name = g.First().Name,
                        Month = g.First().MonthNumber,
                        MonthString = g.First().TimeStamp.ToString("MMM")
                    }).OrderBy(g => g.Month);

                    var avgHour = metricsDto.GroupBy(x => new { x.MetricId, x.MonthDay }).Select(g => new
                    MetricValueDto
                    {
                        Value = g.Sum(s => s.Value)/ hoursPerDay,
                        Name = g.First().Name,
                        Month = g.First().MonthNumber,
                        MonthDay = g.First().MonthDay,
                        MonthString = g.First().TimeStamp.ToString("MMM")
                    }).OrderBy(g => g.Month).ThenBy(g => g.MonthDay);

                    var avgMinute = metricsDto.GroupBy(x => new { x.MetricId, x.MonthDay }).Select(g => new
                    MetricValueDto
                    {
                        Value = g.Sum(s => s.Value) / minutesPerDay,
                        Name = g.First().Name,
                        Month = g.First().MonthNumber,
                        MonthDay = g.First().MonthDay
                    }).OrderBy(g => g.Month).ThenBy(g => g.MonthDay);

                    metricAveragesDto = new MetricAveragesDto
                    {
                        PerDay = avgDay.ToList(),
                        PerHour = avgHour.ToList(),
                        PerMinute = avgMinute.ToList()
                    };
                }

                return metricAveragesDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
