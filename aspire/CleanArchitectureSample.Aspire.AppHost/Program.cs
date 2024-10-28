var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.CleanArchitectureSample_API>("ContactsAPI");

builder.AddProject<Projects.CleanArchitectureSample_Aspire_Web>("ContactsWEB")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
