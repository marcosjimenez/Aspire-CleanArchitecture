using CleanArchitectureSample.Application.Cqrs.Contacts.Commands;
using CleanArchitectureSample.Application.Dtos.Request;

namespace CleanArchitectureSample.UnitTests.Contacts.Commands;

public class UpdateContactCommandHandlerTests
{
    [Fact]
    public async Task Handle_Works()
    {
        var contactRepository = new Mock<IContactRepository>();
        var countryRepository = new Mock<ICountryRepository>();

        var contactName = "Test Contact Name";
        contactRepository.Setup(x => x.Exists(It.IsAny<int>()))
            .Returns(true);

        contactRepository.Setup(x => x.GetByIdWithIncludesAsync(It.IsAny<int>()))
            .ReturnsAsync(new ContactEntity
            {
                Id = 1,
                Name = contactName,
                LastName = "Contact LastName",
                BirthDate = DateTime.Now,
                EMail = "contact@contact.com"
            });

        countryRepository.Setup(x => x.GetById(It.IsAny<int>()))
            .Returns(new CountryEntity
            {
                Id = 1,
                Code = "XX",
                Name = "Test Country"
            });

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ContactProfile());
            cfg.AddProfile(new CountryProfile());
        });
        IMapper mapper = new Mapper(configuration);

        var handler = new UpdateContactCommandHandler(contactRepository.Object, countryRepository.Object, mapper);

        var contactDto = new CreateOrUpdateContactRequest(
            contactName,
            "Test LastName",
            DateTime.Now,
            String.Empty,
            string.Empty,
            null
        );
        var response = await handler.Handle(new UpdateContactCommand(contactDto, 1), new CancellationToken());

        response.Name.Should().Be(contactName);
        response.Country?.Should().NotBeNull();
        response.Country?.Id.Should().Be(1);
    }
}
