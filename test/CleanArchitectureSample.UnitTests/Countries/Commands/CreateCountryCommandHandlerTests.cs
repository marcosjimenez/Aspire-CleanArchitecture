using CleanArchitectureSample.Application.Cqrs.Countries.Commands;
using CleanArchitectureSample.Application.Dto.Request;

namespace CleanArchitectureSample.UnitTests.Countries.Commands;

public class CreateCountryCommandHandlerTests
{

    [Fact]
    public async Task Handle_Works()
    {
        var countryRepository = new Mock<ICountryRepository>();

        var countryName = "Test Country Name";
        countryRepository.Setup(x => x.Create(It.IsAny<CountryEntity>()))
            .Returns(1);

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new CountryProfile());
        });
        IMapper mapper = new Mapper(configuration);

        var handler = new CreateCountryCommandHandler(countryRepository.Object, mapper);

        var countryDto = new CreateOrUpdateCountryRequest(
            countryName,
            "XX"
        );
        var response = await handler.Handle(new CreateCountryCommand(countryDto), new CancellationToken());

        response.Name.Should().Be(countryName);
    }
}
