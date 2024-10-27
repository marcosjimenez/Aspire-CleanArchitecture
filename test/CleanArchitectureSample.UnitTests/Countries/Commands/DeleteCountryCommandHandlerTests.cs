using CleanArchitectureSample.Application.Cqrs.Countries.Commands;

namespace CleanArchitectureSample.UnitTests.Countries.Commands;

public class DeleteCountryCommandHandlerTests
{
    [Fact]
    public async Task Handle_Works()
    {
        var countryRepository = new Mock<ICountryRepository>();

        countryRepository.Setup(x => x.Exists(It.IsAny<int>()))
            .Returns(true);

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new CountryProfile());
        });
        IMapper mapper = new Mapper(configuration);

        var handler = new DeleteCountryCommandHandler(countryRepository.Object);

        var response = await handler.Handle(new DeleteCountryCommand(1), new CancellationToken());

        response.Should().BeTrue();
    }
}
