using Microsoft.AspNetCore.Mvc;
using MetricsAPI.DTOs;
using Mapster;
using MetricsAPI.Models;
using MetricsAPI.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using MetricsAPI.Services;

namespace MetricsAPI.Controllers
{
    [EnableCors]    
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly MetricsDbContext _dbContext;
        private IMetricsHelper _metricsHelper;
        public MetricsController(MetricsDbContext dbContext, IMetricsHelper metricsHelper)
        {
            _dbContext = dbContext;
            _metricsHelper = metricsHelper;
        }
                
        [HttpPost("AddMetricDefinition")]
        public MetricDefinitionDto AddMetricDefinition([FromBody] MetricDefinitionDto metricDto)
        {
            MetricDefinitionDto response = new MetricDefinitionDto();

            var metric = metricDto.Adapt<MetricDefinition>();

            try
            {
                _dbContext.MetricDefinitions.Add(metric);
                var result = _dbContext.SaveChanges();
                response = metric.Adapt<MetricDefinitionDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
        public async Task<MetricDisplayDto> AddMetricValue([FromBody] MetricDto metricDto)
        {
            var metric = metricDto.Adapt<Metric>();
            MetricDisplayDto response = new MetricDisplayDto();

            //persist to DB
            try
            {
                _dbContext.Metrics.Add(metric);
                var result = await _dbContext.SaveChangesAsync();

                if(result == 1)
                {
                    var dbMetric = await _dbContext.FindAsync<Metric>(metric.Id);
                    response = dbMetric.Adapt<MetricDisplayDto>();
                }
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

        [HttpGet("GetMetricAverages")]
        public async Task<MetricAveragesDto> GetMetricAverages()
        {
            var metrics = await _dbContext.Metrics.ToListAsync();
            var result = await _metricsHelper.GetMetricsAverages(metrics);

            return result;
        }


    }
}
