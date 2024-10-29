using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureSample.Application.Contracts;
using CleanArchitectureSample.Infrastructure.Database;
using CleanArchitectureSample.Infrastructure.Database.Repository;
using CleanArchitectureSample.Core.Aggregates;
using Bogus;
using Microsoft.Extensions.Hosting;

namespace CleanArchitectureSample.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.AddRedisDistributedCache("cache");

        var connectionString = builder.Configuration.GetConnectionString(InfrastructureConstants.Database.DefaultConnection);
        builder.Services.AddDbContext<CleanArchitectureSampleDbContext>(options =>
            options
                .UseSqlite(connectionString)
                .EnableSensitiveDataLogging(true)
            );

        builder.Services
            .AddScoped<ICacheManager, CacheManager>()
            .AddScoped<ICountryRepository, CountryRepository>()
            .AddScoped<IContactRepository, ContactRepository>();

        return builder;
    }

    public static  CleanArchitectureSampleDbContext SeedFakeData(
        this CleanArchitectureSampleDbContext context,
        int maxCountries = 1,
        int maxContacts = 1)
    {
        // Countries
        if (maxCountries > 0)
        {
            Faker.GlobalUniqueIndex = context.Country.Max(x => (int?)x.Id) ?? 0; 
            var testCountries = new Faker<CountryEntity>()
                .RuleFor(o => o.Id, f => f.UniqueIndex)
                .RuleFor(o => o.Name, f => f.Address.Country())
                .RuleFor(o => o.Code, f => f.Address.CountryCode())
                .RuleFor(o => o.TimeStamp, f => f.Date.Recent(0));
            var countries = testCountries.Generate(100);
            context.Country.AddRange(countries);
            context.SaveChanges();

            // Contacts
            if (maxContacts > 0)
            {
                Faker.GlobalUniqueIndex = context.Contact.Max(x => (int?)x.Id) ?? 0;
                var testContacts = new Faker<ContactEntity>()
                    .RuleFor(o => o.Id, f => f.UniqueIndex)
                    .RuleFor(o => o.Name, f => f.Name.FirstName())
                    .RuleFor(o => o.LastName, f => f.Name.LastName())
                    .RuleFor(o => o.EMail, f => f.Internet.Email())
                    .RuleFor(o => o.BirthDate, f => f.Person.DateOfBirth)
                    .RuleFor(o => o.Country, f => f.PickRandom(countries))
                    .RuleFor(o => o.Phone, f => f.Person.Phone)
                    .RuleFor(o => o.TimeStamp, f => f.Date.Recent(0));
                var contacts = testContacts.Generate(1000);
                context.Contact.AddRange(contacts);
                context.SaveChanges();
            }
        }

        return context;
    }
}