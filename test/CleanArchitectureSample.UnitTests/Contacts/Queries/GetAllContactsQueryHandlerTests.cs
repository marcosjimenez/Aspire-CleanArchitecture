using CleanArchitectureSample.Application.Cqrs.Contacts.Queries;
using CleanArchitectureSample.Application.Dto.Response;

namespace CleanArchitectureSample.UnitTests.Contacts.Queries
{
    public class GetAllContactsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Works()
        {
            var contactRepository = new Mock<IContactRepository>();
            var cacheService = new Mock<ICacheService> { DefaultValueProvider = new ZeroDefaultValueProvider() };

            contactRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(GetAllContacts());

            cacheService.Setup(x => x.GetAsync<IEnumerable<ContactResponse>?>(It.IsAny<string>()))
                .ReturnsAsync((IEnumerable<ContactResponse>?)null);
            cacheService.Setup(x => x.SetAsync(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(Task.CompletedTask);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ContactProfile());
                cfg.AddProfile(new CountryProfile());
            });
            IMapper mapper = new Mapper(configuration);

            var handler = new GetAllContactsQueryHandler(contactRepository.Object, cacheService.Object, mapper);

            var response = await handler.Handle(new GetAllContactsQuery(), new CancellationToken());

            response.Should().NotBeNull();
            response.Count().Should().BeGreaterThan(0);
        }

        private static IEnumerable<ContactEntity> GetAllContacts()
            => [
                CreateTestContact(1),
                CreateTestContact(2),
                CreateTestContact(3),
                CreateTestContact(4),
                CreateTestContact(5)
            ];

        private static ContactEntity CreateTestContact(int id)
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