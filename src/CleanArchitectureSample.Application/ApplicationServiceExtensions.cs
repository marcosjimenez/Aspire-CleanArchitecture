using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitectureSample.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddMediatR(x => x.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()))
            .AddAutoMapper(Assembly.GetExecutingAssembly());

    }
}
