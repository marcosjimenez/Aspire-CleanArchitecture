using FluentAssertions;
using System.Net.Http.Json;
using CleanArchitectureSample.FunctionalTests.Infrastructure;
using CleanArchitectureSample.Application.Dto.Response;

namespace CleanArchitectureSample.FunctionalTests.Controllers
{
    [Collection("Contacts")]
    public class CountryControllerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
    {

        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task GetAll_Returns_OK()
        {
            var data = await _client.GetFromJsonAsync<List<CountryResponse>>("api/country");

            data.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetById_Returns_OK()
        {
            var data = await _client.GetFromJsonAsync<CountryResponse>("api/country/1");

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
            var data = await _client.PostAsJsonAsync("api/country", country);

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
            var data = await _client.PutAsJsonAsync("api/country/1", country);

            data.IsSuccessStatusCode.Should().BeTrue();

            var createdCountry = await data.Content.ReadFromJsonAsync<CountryResponse>();
            createdCountry?.Name.Should().Be(name);
        }

        [Fact]
        public async Task DeleteCountry_Returns_Ok()
        {
            var data = await _client.DeleteAsync("api/contact/100");
            data.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}