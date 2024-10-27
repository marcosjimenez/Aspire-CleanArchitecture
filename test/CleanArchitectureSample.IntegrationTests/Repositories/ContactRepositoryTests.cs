using FluentAssertions;
using CleanArchitectureSample.Core.Aggregates;
using CleanArchitectureSample.Infrastructure.Database.Repository;

namespace CleanArchitectureSample.IntegrationTests.Repositories;

public class ContactRepositoryTests : CleanArchitectureSampleFixture
{

    [Fact]
    public async Task AddAsync_Works()
    {
        var repository = new ContactRepository(_context);
        var contact = CreateTestContact(1);

        repository.Create(contact);
        repository.Save();

        var insertedContact = await repository.GetByIdAsync(1);
        insertedContact.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAllAsync_Works()
    {
        var repository = new ContactRepository(_context);
        repository.Create(CreateTestContact(2));
        repository.Create(CreateTestContact(3));
        repository.Create(CreateTestContact(4));
        repository.Create(CreateTestContact(5));
        repository.Create(CreateTestContact(6));
        await repository.SaveAsync();

        var insertedContacts = await repository.GetAllAsync();
        insertedContacts.Count().Should().Be(5);
    }

    [Fact]
    public async Task Create_Works()
    {
        var repository = new ContactRepository(_context);
        repository.Create(CreateTestContact(7));
        repository.Create(CreateTestContact(8));
        repository.Create(CreateTestContact(9));
        repository.Create(CreateTestContact(10));
        repository.Create(CreateTestContact(11));
        await repository.SaveAsync();

        repository.GetById(7)
            .Should().NotBeNull();
        repository.GetById(8)
            .Should().NotBeNull();
        repository.GetById(9)
            .Should().NotBeNull();
        repository.GetById(10)
            .Should().NotBeNull();
        repository.GetById(11)
            .Should().NotBeNull();
    }

    [Fact]
    public async Task Exists_Works()
    {
        var repository = new ContactRepository(_context);
        repository.Create(CreateTestContact(12));
        await repository.SaveAsync();

        repository.Exists(12).Should().BeTrue();
    }

    [Fact]
    public async Task GetByIdAsync_Works()
    {
        var repository = new ContactRepository(_context);

        repository.Create(CreateTestContact(13));
        await repository.SaveAsync();

        var contact = await repository.GetByIdAsync(13);
        contact.Should().NotBeNull();
        contact?.Id.Should().Be(13);
    }

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