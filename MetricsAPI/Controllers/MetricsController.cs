using Microsoft.AspNetCore.Mvc;
using MetricsAPI.DTOs;
using Mapster;
using MetricsAPI.Models;
using MetricsAPI.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace MetricsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly MetricsDbContext _dbContext;
        public MetricsController(MetricsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
                
        [HttpPost("AddMetricDefinition")]
        public MetricDefinitionDto AddMetricDefinition([FromBody] MetricDefinitionDto metricDto)
        {
            var metric = metricDto.Adapt<MetricDefinition>();
            MetricDefinitionDto response = new MetricDefinitionDto();

            //persist to DB
            try
            {
                _dbContext.MetricDefinitions.Add(metric);
                var result = _dbContext.SaveChanges();
                response = metric.Adapt<MetricDefinitionDto>();
            }
            catch (Exception ex)
            {
                //add message
            }

            return response;
        }

        [HttpGet("AllMetricDefinitions")]
        public async Task<IList<MetricDefinitionDto>> GetAllMetricDefinitions()
        {
            var result = await _dbContext.MetricDefinitions.ToListAsync();
            return result.Adapt<List<MetricDefinitionDto>>();
        }

        [HttpPost("AddMetricValue")]
        public MetricDisplayDto AddMetricValue([FromBody] MetricDto metricDto)
        {
            var metric = metricDto.Adapt<Metric>();
           MetricDisplayDto response = new MetricDisplayDto();

            //persist to DB
            try
            {
                _dbContext.Metrics.Add(metric);
                var result = _dbContext.SaveChanges();
                response = metric.Adapt<MetricDisplayDto>();
            }
            catch (Exception ex)
            {
                //add message
            }

            return response;
        }
                
        [HttpGet("GetAllMetrics")]
        public async Task<List<MetricDisplayDto>> GetAllMetricValues()
        {
            var result = await _dbContext.Metrics.ToListAsync();
            return result.Adapt<List<MetricDisplayDto>>();
        }


    }
}
