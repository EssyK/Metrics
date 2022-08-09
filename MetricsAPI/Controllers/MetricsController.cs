using Mapster;
using MetricsAPI.DataLayer;
using MetricsAPI.DTOs;
using MetricsAPI.Models;
using MetricsAPI.Response;
using MetricsAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
        public BaseResponse<MetricDefinitionDto> AddMetricDefinition([FromBody] MetricDefinitionDto metricDto)
        {
            MetricDefinitionDto metricDefinitionDto;
            var metric = metricDto.Adapt<MetricDefinition>();

            try
            {
                _dbContext.MetricDefinitions.Add(metric);
                var result = _dbContext.SaveChanges();
                metricDefinitionDto = metric.Adapt<MetricDefinitionDto>();

                return new BaseResponse<MetricDefinitionDto>
                {
                    Data = metricDefinitionDto,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MetricDefinitionDto>
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = ex.Message
                };
            }
        }

        [HttpGet("AllMetricDefinitions")]
        public async Task<BaseResponse<IList<MetricDefinitionDto>>> GetAllMetricDefinitions()
        {
            try
            {
                var metricDefinitions = await _dbContext.MetricDefinitions.ToListAsync();
                var metricDtoDefinitions = metricDefinitions.Adapt<List<MetricDefinitionDto>>();

                return new BaseResponse<IList<MetricDefinitionDto>>
                {
                    Data = metricDtoDefinitions,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IList<MetricDefinitionDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        [HttpPost("AddMetricValue")]
        public async Task<BaseResponse<MetricDisplayDto>> AddMetricValue([FromBody] MetricDto metricDto)
        {
            var metric = metricDto.Adapt<Metric>();
            MetricDisplayDto response;

            try
            {
                _dbContext.Metrics.Add(metric);
                var result = await _dbContext.SaveChangesAsync();

                if (result == 1)
                {
                    var dbMetric = await _dbContext.FindAsync<Metric>(metric.Id);
                    response = dbMetric.Adapt<MetricDisplayDto>();

                    return new BaseResponse<MetricDisplayDto>
                    {
                        Data = response,
                        StatusCode = HttpStatusCode.OK
                    };
                }
                else
                {
                    return new BaseResponse<MetricDisplayDto>
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Metric was not saved. Kindly retry."
                    };

                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<MetricDisplayDto>
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpGet("GetAllMetrics")]
        public async Task<BaseResponse<List<MetricDisplayDto>>> GetAllMetricValues()
        {
            try
            {
                var result = await _dbContext.Metrics.ToListAsync();
                var metricDto = result.Adapt<List<MetricDisplayDto>>();

                return new BaseResponse<List<MetricDisplayDto>>
                {
                    Data = metricDto,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<MetricDisplayDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        [HttpGet("GetMetricAverages")]
        public async Task<BaseResponse<MetricAveragesDto>> GetMetricAverages()
        {
            try
            {
                var metrics = await _dbContext.Metrics.ToListAsync();
                var result = await _metricsHelper.GetMetricsAverages(metrics);

                return new BaseResponse<MetricAveragesDto>
                {
                    Data = result,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MetricAveragesDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }


    }
}
