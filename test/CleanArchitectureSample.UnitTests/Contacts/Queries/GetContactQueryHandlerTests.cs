using CleanArchitectureSample.Application.Cqrs.Contacts.Queries;

namespace CleanArchitectureSample.UnitTests.Contacts.Queries
{
    public class GetContactQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Works()
        {
            var contactRepository = new Mock<IContactRepository>();

            contactRepository.Setup(x => x.GetByIdWithIncludesAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => CreateTestContact(id));

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ContactProfile());
                cfg.AddProfile(new CountryProfile());
            });
            IMapper mapper = new Mapper(configuration);

            var handler = new GetContactQueryHandler(contactRepository.Object, mapper);

            var response = await handler.Handle(new GetContactQuery(1), new CancellationToken());

            response.Should().NotBeNull();
            response.Id.Should().Be(1);
        }

        private static ContactEntity? CreateTestContact(int id)
        => new()
        {
            Id = id,
            Name = $"Fake name {id}",
            BirthDate = DateTime.Now,
            EMail = $"fakemail{id}@fakeserver.com",
            Country = new CountryEntity
            {
                Id = id,
                Name = $"Fake country {id}"
            }
        };
    }
}