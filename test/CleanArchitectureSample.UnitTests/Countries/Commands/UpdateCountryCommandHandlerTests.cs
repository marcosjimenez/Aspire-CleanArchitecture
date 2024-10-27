using CleanArchitectureSample.Application.Cqrs.Countries.Commands;
using CleanArchitectureSample.Application.Dtos.Request;

namespace CleanArchitectureSample.UnitTests.Countries.Commands;

public class UpdateCountryCommandHandlerTests
{
    [Fact]
    public async Task Handle_Works()
    {
        var countryRepository = new Mock<ICountryRepository>();

        var countryName = "Test Country Name";
        countryRepository.Setup(x => x.Exists(It.IsAny<int>()))
            .Returns(true);

        countryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new CountryEntity
            {
                Id = 1,
                Name = countryName,
                Code = "XX"
            });

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new CountryProfile());
        });
        IMapper mapper = new Mapper(configuration);

        var handler = new UpdateCountryCommandHandler(countryRepository.Object, mapper);

        var contactDto = new CreateOrUpdateCountryRequest(
            countryName,
            "XX"
        );
        var response = await handler.Handle(new UpdateCountryCommand(contactDto, 1), new CancellationToken());

        response.Name.Should().Be(countryName);
    }
}
