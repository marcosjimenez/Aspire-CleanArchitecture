using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CleanArchitectureSample.Application.Mappers;
using CleanArchitectureSample.Infrastructure.Database;
using CleanArchitectureSample.Infrastructure;

namespace CleanArchitectureSample.FunctionalTests.Infrastructure;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Test");
        var host = builder.Build();
        host.Start();

        var serviceProvider = host.Services;
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<CleanArchitectureSampleDbContext>();
            var mapper = scopedServices.GetRequiredService<IMapper>();

            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory>>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.SeedFakeData(50,100);
        }

        return host;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(services =>
            {
                // Remove original dbContext to use InMemory
                RemoveService(services, typeof(DbContextOptions<CleanArchitectureSampleDbContext>));
                // Remove original Mapper to avoid assembly load errors
                RemoveService(services, typeof(IMapper));

                services.AddDbContext<CleanArchitectureSampleDbContext>(options =>
                {
                    options.UseInMemoryDatabase("Contacts");
                });

                var mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile(new ContactProfile());
                    c.AddProfile(new CountryProfile());
                });
                var mapper = mapperConfiguration.CreateMapper();
                services.AddSingleton(mapper);
            });
    }

    private static void RemoveService(IServiceCollection services, Type typeToRemove)
    {
        var contextService = services.SingleOrDefault(x => x.ServiceType == typeToRemove);
        if (contextService is not null)
            services.Remove(contextService);
    }
}
