using CleanArchitectureSample.API;
using CleanArchitectureSample.Application;
using CleanArchitectureSample.Aspire.ServiceDefaults;
using CleanArchitectureSample.Infrastructure;
using CleanArchitectureSample.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile(ApiConstants.Configuration.DefaultSettingsFile, optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddApiSecurity(builder.Configuration[ApiConstants.Configuration.ApiKeySection] ?? String.Empty)
    .AddApplicationServices();

builder.AddInfrastructureServices();

var app = builder.Build();

// Ensure DB
using var scope = app.Services.CreateScope();
var dbcontext = scope.ServiceProvider.GetRequiredService<CleanArchitectureSampleDbContext>();
dbcontext.Database.EnsureCreated();

if (app.Environment.IsDevelopment())
{
    // Fake data for Dev
    if (!dbcontext.Country.Any() && !dbcontext.Contact.Any())
        dbcontext.SeedFakeData(25, 100);

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { } // For tests