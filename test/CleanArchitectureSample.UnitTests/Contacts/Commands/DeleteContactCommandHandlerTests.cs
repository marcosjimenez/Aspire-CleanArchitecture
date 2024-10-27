using CleanArchitectureSample.Application.Cqrs.Contacts.Commands;
using CleanArchitectureSample.Application.Dtos.Request;

namespace CleanArchitectureSample.UnitTests.Contacts.Commands;

public class DeleteContactCommandHandlerTests
{
    [Fact]
    public async Task Handle_Works()
    {
        var contactRepository = new Mock<IContactRepository>();

        contactRepository.Setup(x => x.Exists(It.IsAny<int>()))
            .Returns(true);

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ContactProfile());
            cfg.AddProfile(new CountryProfile());
        });
        IMapper mapper = new Mapper(configuration);

        var handler = new DeleteContactCommandHandler(contactRepository.Object);

        var response = await handler.Handle(new DeleteContactCommand(1), new CancellationToken());

        response.Should().BeTrue();
    }
}
