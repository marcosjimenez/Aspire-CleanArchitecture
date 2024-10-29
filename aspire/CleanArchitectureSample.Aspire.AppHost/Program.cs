using CleanArchitectureSample.Aspire.Common;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis(ResourceNames.RedisCache);
var sqlServer = builder.AddSqlServer(ResourceNames.SqlServer)
    .WithDataVolume()
    .PublishAsContainer();
var sqlDb = sqlServer.AddDatabase(ResourceNames.SqlDatabase);

var apiService = builder.AddProject<Projects.CleanArchitectureSample_API>(ResourceNames.ContactsAPI)
    .WithReference(cache)
    .WithReference(sqlDb);

builder.AddProject<Projects.CleanArchitectureSample_Aspire_Web>(ResourceNames.ContactsWEB)
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
