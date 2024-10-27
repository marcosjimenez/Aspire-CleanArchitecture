using FluentAssertions;
using CleanArchitectureSample.Core.Aggregates;
using CleanArchitectureSample.Infrastructure.Database.Repository;

namespace CleanArchitectureSample.IntegrationTests.Repositories;

public class CountryRepositoryTests : CleanArchitectureSampleFixture
{

    [Fact]
    public async Task AddAsync_Works()
    {
        var repository = new CountryRepository(_context);
        var country = CreateTestCountry(1);

        repository.Create(country);
        repository.Save();

        var insertedCountry = await repository.GetByIdAsync(1);
        insertedCountry.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAllAsync_Works()
    {
        var repository = new CountryRepository(_context);
        var country2 = CreateTestCountry(2);
        var country3 = CreateTestCountry(3);
        var country4 = CreateTestCountry(4);
        var country5 = CreateTestCountry(5);
        var country6 = CreateTestCountry(6);

        repository.Create(country2);
        repository.Create(country3);
        repository.Create(country4);
        repository.Create(country5);
        repository.Create(country6);

        await repository.SaveAsync();

        var insertedCountries = await repository.GetAllAsync();
        insertedCountries.Should().Contain(country2);
        insertedCountries.Should().Contain(country3);
        insertedCountries.Should().Contain(country4);
        insertedCountries.Should().Contain(country5);
        insertedCountries.Should().Contain(country6);
    }

    [Fact]
    public async Task Create_Works()
    {
        var repository = new CountryRepository(_context);

        var country7 = CreateTestCountry(7);
        var country8 = CreateTestCountry(8);
        var country9 = CreateTestCountry(9);
        var country10 = CreateTestCountry(10);
        var country11 = CreateTestCountry(11);

        repository.Create(country7);
        repository.Create(country8);
        repository.Create(country9);
        repository.Create(country10);
        repository.Create(country11);
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
        var repository = new CountryRepository(_context);
        repository.Create(CreateTestCountry(12));
        await repository.SaveAsync();

        repository.Exists(12).Should().BeTrue();
    }

    [Fact]
    public async Task GetByIdAsync_Works()
    {
        var repository = new CountryRepository(_context);

        repository.Create(CreateTestCountry(13));
        await repository.SaveAsync();

        var contact = await repository.GetByIdAsync(13);
        contact.Should().NotBeNull();
        contact?.Id.Should().Be(13);
    }

    private static CountryEntity CreateTestCountry(int id)
       => new()
       {
           Id = id,
           Name = $"Fake name {id}",
           Code = Random.Shared.Next(99).ToString()
       };
}