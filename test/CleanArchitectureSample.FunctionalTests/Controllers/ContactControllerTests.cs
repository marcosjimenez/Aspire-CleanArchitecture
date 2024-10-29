using FluentAssertions;
using System.Net.Http.Json;
using CleanArchitectureSample.FunctionalTests.Infrastructure;
using CleanArchitectureSample.Application.Dto.Response;

namespace CleanArchitectureSample.FunctionalTests.Controllers
{
    [Collection("Contacts")]
    public class ContactControllerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
    {

        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task GetAll_Returns_OK()
        {
            var data = await _client.GetFromJsonAsync<List<ContactResponse>>("api/contact");

            data.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetById_Returns_OK()
        {
            var data = await _client.GetFromJsonAsync<ContactResponse>("api/contact/1");

            data.Should().NotBeNull();
            data?.Id.Should().Be(1);
        }

        [Fact]
        public async Task CreateContact_Returns_Ok()
        {
            var name = Guid.NewGuid().ToString();
            var contact = new
            {
                Name = name,
                LastName = $"Last {name}"
            };
            var data = await _client.PostAsJsonAsync("api/contact", contact);

            data.IsSuccessStatusCode.Should().BeTrue();

            var createdContact = await data.Content.ReadFromJsonAsync<ContactResponse>();
            createdContact?.Name.Should().Be(name);
        }

        [Fact]
        public async Task UpdateContact_Returns_Ok()
        {
            var name = Guid.NewGuid().ToString();
            var contact = new
            {
                Name = name,
                LastName = $"Last {name}"
            };
            var data = await _client.PutAsJsonAsync("api/contact/1", contact);

            data.IsSuccessStatusCode.Should().BeTrue();

            var createdContact = await data.Content.ReadFromJsonAsync<ContactResponse>();
            createdContact?.Name.Should().Be(name);
        }

        [Fact]
        public async Task DeleteContact_Returns_Ok()
        {
            var data = await _client.DeleteAsync("api/contact/100");
            data.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}