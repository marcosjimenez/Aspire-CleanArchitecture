using Microsoft.EntityFrameworkCore;
using CleanArchitectureSample.Infrastructure.Database;

namespace CleanArchitectureSample.IntegrationTests;

public abstract class CleanArchitectureSampleFixture
{
    protected readonly CleanArchitectureSampleDbContext _context;

    protected CleanArchitectureSampleFixture()
    {
        var serviceProvider = new ServiceCollection()
        .AddEntityFrameworkInMemoryDatabase()
        .BuildServiceProvider();

        var builder = new DbContextOptionsBuilder<CleanArchitectureSampleDbContext>();
        builder.UseInMemoryDatabase("Contacts")
               .UseInternalServiceProvider(serviceProvider);

        var options = builder.Options;
        _context = new CleanArchitectureSampleDbContext(options);
    }
}
