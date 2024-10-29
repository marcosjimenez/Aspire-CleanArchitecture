using FluentAssertions;
using System.Net.Http.Json;
using CleanArchitectureSample.FunctionalTests.Infrastructure;
using CleanArchitectureSample.Application.Dto.Response;

namespace CleanArchitectureSample.FunctionalTests.Controllers;

[Collection("Contacts")]
public class CountryControllerTests(TestApplicationHost appHost) : IClassFixture<TestApplicationHost>
{

    [Fact]
    public async Task GetAll_Returns_OK()
    {
        var data = await appHost.ApiClient.GetFromJsonAsync<List<CountryResponse>>("api/country");

        data.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetById_Returns_OK()
    {
        var data = await appHost.ApiClient.GetFromJsonAsync<CountryResponse>("api/country/1");

        data.Should().NotBeNull();
        data?.Id.Should().Be(1);
    }

    [Fact]
    public async Task CreateCountry_Returns_Ok()
    {
        var name = Guid.NewGuid().ToString();
        var country = new
        {
            Name = name
        };
        var data = await appHost.ApiClient.PostAsJsonAsync("api/country", country);

        data.IsSuccessStatusCode.Should().BeTrue();

        var createdCountry = await data.Content.ReadFromJsonAsync<CountryResponse>();
        createdCountry?.Name.Should().Be(name);
    }

    [Fact]
    public async Task UpdateCountry_Returns_Ok()
    {
        var name = Guid.NewGuid().ToString();
        var country = new
        {
            Name = name
        };
        var data = await appHost.ApiClient.PutAsJsonAsync("api/country/1", country);

        data.IsSuccessStatusCode.Should().BeTrue();

        var createdCountry = await data.Content.ReadFromJsonAsync<CountryResponse>();
        createdCountry?.Name.Should().Be(name);
    }

    [Fact]
    public async Task DeleteInUseCountry_Returns_False()
    {
        var data = await appHost.ApiClient.DeleteAsync("api/country/1");
        data.IsSuccessStatusCode.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteCountry_Returns_Ok()
    {
        var name = Guid.NewGuid().ToString();
        var country = new
        {
            Name = name
        };
        var data = await appHost.ApiClient.PostAsJsonAsync("api/country", country);
        var createdCountry = await data.Content.ReadFromJsonAsync<CountryResponse>();
        
        data = await appHost.ApiClient.DeleteAsync($"api/country/{createdCountry?.Id}");
        data.IsSuccessStatusCode.Should().BeTrue();
    }

}