using Mapster;
using MetricsAPI.DTOs;
using System.Reflection;

namespace MetricsAPI.Configuration
{
    public static class MapsterConfiguration
    {
        public static void AddMapster(this IServiceCollection services)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            Assembly applicationAssembly = typeof(BaseDto<,>).Assembly;
            typeAdapterConfig.Scan(applicationAssembly);
        }
    }

}
