using CleanArchitectureSample.Application.Contracts;
using CleanArchitectureSample.Core.Aggregates;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitectureSample.Infrastructure.Database.Repository;

public class CountryRepository(CleanArchitectureSampleDbContext dbContext) : ICountryRepository, IDisposable
{
    private readonly CleanArchitectureSampleDbContext _dbContext = dbContext ??
    throw new ArgumentNullException(nameof(dbContext));

    private bool disposed = false;

    public int Create(CountryEntity entity)
    {
        _dbContext.Country.Add(entity);
        return entity.Id;
    }

    public void Update(CountryEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public bool Delete(int id)
    {
        var item = _dbContext.Country.Find(id);
        if (item is null)
            return false;

        _dbContext.Country.Remove(item);
        return true;
    }

    public bool Exists(int id)
        => _dbContext.Country.Where(x => x.Id == id).Count() > 0;

    public IEnumerable<CountryEntity> GetAll()
        => _dbContext.Country
        .ToList();

    public async Task<IEnumerable<CountryEntity>> GetAllAsync()
        => await _dbContext.Country
        .ToListAsync();

    public CountryEntity? GetById(int id)
        => _dbContext.Country.
            FirstOrDefault(x => x.Id == id);

    public async Task<CountryEntity?> GetByIdAsync(int id)
        => await _dbContext.Country.
        FirstOrDefaultAsync(x => x.Id == id);

    public CountryEntity? GetByIdWithIncludes(int id)
        => GetById(id);

    public async Task<CountryEntity?> GetByIdWithIncludesAsync(int id)
        => await GetByIdAsync(id);

    public int Save()
        => _dbContext.SaveChanges();

    public async Task<int> SaveAsync()
        => await _dbContext.SaveChangesAsync();

    public CountryEntity? Select(Expression<Func<CountryEntity?, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return _dbContext.Country
            .Where(predicate)
            .FirstOrDefault()!;
    }

    public async Task<CountryEntity?> SelectAsync(Expression<Func<CountryEntity?, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return await _dbContext.Country
            .Where(predicate)
            .FirstOrDefaultAsync()!;
    }

    public int Count()
        => _dbContext.Country.Count();

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}
