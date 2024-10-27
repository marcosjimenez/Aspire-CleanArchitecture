using CleanArchitectureSample.Application.Cqrs.Countries.Queries;

namespace CleanArchitectureSample.UnitTests.Countries.Queries
{
    public class GetCountryQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Works()
        {
            var countryRepository = new Mock<ICountryRepository>();

            countryRepository.Setup(x => x.GetByIdWithIncludesAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => CreateTestCountry(id));

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CountryProfile());
            });
            IMapper mapper = new Mapper(configuration);

            var handler = new GetCountryQueryHandler(countryRepository.Object, mapper);

            var response = await handler.Handle(new GetCountryQuery(1), new CancellationToken());

            response.Should().NotBeNull();
            response.Id.Should().Be(1);
        }

        private static CountryEntity CreateTestCountry(int id)
        => new()
        {
            Id = id,
            Name = $"Fake Country {id}",
            Code = id.ToString()
        };
    }
}