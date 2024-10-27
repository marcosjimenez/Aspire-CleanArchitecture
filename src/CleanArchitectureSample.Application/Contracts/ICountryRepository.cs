using CleanArchitectureSample.Core.Aggregates;

namespace CleanArchitectureSample.Application.Contracts
{
    public interface ICountryRepository : IBaseRepository<int, CountryEntity>
    {

    }
}
