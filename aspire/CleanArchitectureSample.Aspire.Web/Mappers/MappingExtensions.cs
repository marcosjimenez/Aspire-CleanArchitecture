using System.Reflection;

namespace CleanArchitectureSample.Aspire.Web.Mappers;

public static class MappingExtensions
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        return services
            .AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
