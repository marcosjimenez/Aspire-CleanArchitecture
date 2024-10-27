using CleanArchitectureSample.Application.Cqrs.Countries.Queries;

namespace CleanArchitectureSample.UnitTests.Countries.Queries
{
    public class GetAllCountriesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Works()
        {
            var countryRepository = new Mock<ICountryRepository>();

            countryRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(GetAllCountries());

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CountryProfile());
            });
            IMapper mapper = new Mapper(configuration);

            var handler = new GetAllCountriesQueryHandler(countryRepository.Object, mapper);

            var response = await handler.Handle(new GetAllCountriesQuery(), new CancellationToken());

            response.Should().NotBeNull();
            response.Count().Should().BeGreaterThan(0);
        }

        private static IEnumerable<CountryEntity> GetAllCountries()
            => [
                CreateTestCountry(1),
                CreateTestCountry(2),
                CreateTestCountry(3),
                CreateTestCountry(4),
                CreateTestCountry(5)
            ];

        private static CountryEntity CreateTestCountry(int id)
        => new()
        {
            Id = id,
            Name = $"Fake Country {id}",
            Code = id.ToString()
        };
    }
}