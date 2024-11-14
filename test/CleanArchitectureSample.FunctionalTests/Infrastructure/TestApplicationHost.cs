using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Testing;
using CleanArchitectureSample.Aspire.Common;

namespace CleanArchitectureSample.FunctionalTests.Infrastructure;

public class TestApplicationHost : IAsyncLifetime
{
    private DistributedApplication app = default!;
    private HttpClient apiClient = default!;

    public HttpClient ApiClient { get => apiClient; }

    public async Task DisposeAsync()
    {
        await app.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.CleanArchitectureSample_Aspire_AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        app = await appHost.BuildAsync();
        var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
        await app.StartAsync();

        await resourceNotificationService.WaitForResourceAsync(ResourceNames.ContactsAPI, KnownResourceStates.Running)
            .WaitAsync(TimeSpan.FromSeconds(30));

        apiClient = app.CreateHttpClient(ResourceNames.ContactsAPI);
    }
}
