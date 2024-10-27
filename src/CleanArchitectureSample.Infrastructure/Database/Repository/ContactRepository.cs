using CleanArchitectureSample.Application.Contracts;
using CleanArchitectureSample.Core.Aggregates;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitectureSample.Infrastructure.Database.Repository;

public class ContactRepository(CleanArchitectureSampleDbContext dbContext) : IContactRepository, IDisposable
{
    private readonly CleanArchitectureSampleDbContext _dbContext = dbContext ??
    throw new ArgumentNullException(nameof(dbContext));

    private bool disposed = false;

    public void CreateFakeContacts(int contactCount)
    {
        _dbContext.SeedFakeData(contactCount, contactCount);
    }

    public int Create(ContactEntity entity)
    {
        _dbContext.Contact.Add(entity);
        return entity.Id;
    }

    public void Update(ContactEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public bool Delete(int id)
    {
        var item = _dbContext.Contact.Find(id);
        if (item is null)
            return false;

        _dbContext.Contact.Remove(item);
        return true;
    }

    public bool Exists(int id)
        => _dbContext.Contact.Any(x => x.Id == id);

    public IEnumerable<ContactEntity> GetAll()
        => _dbContext.Contact
        .Include(x => x.Country)
        .ToList();

    public async Task<IEnumerable<ContactEntity>>  GetAllAsync()
        => await _dbContext.Contact
        .Include(x => x.Country)
        .ToListAsync();

    public ContactEntity? GetById(int id)
        => _dbContext.Contact.
            FirstOrDefault(x => x.Id == id);

    public async Task<ContactEntity?> GetByIdAsync(int id)
        => await _dbContext.Contact.
        FirstOrDefaultAsync(x => x.Id == id);

    public ContactEntity? GetByIdWithIncludes(int id)
        => _dbContext.Contact
            .Include(x => x.Country)
            .FirstOrDefault(x => x.Id == id);

    public async Task<ContactEntity?> GetByIdWithIncludesAsync(int id)
        => await _dbContext.Contact
            .Include(x => x.Country)
            .FirstOrDefaultAsync(x => x.Id == id);

    public int Save()
        => _dbContext.SaveChanges();

    public async Task<int> SaveAsync()
        => await _dbContext.SaveChangesAsync();

    public ContactEntity? Select(Expression<Func<ContactEntity?, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return _dbContext.Contact
            .Where(predicate)
            .FirstOrDefault()!;
    }

    public async Task<ContactEntity?> SelectAsync(Expression<Func<ContactEntity?, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return await _dbContext.Contact
            .Where(predicate)
            .FirstOrDefaultAsync()!;
    }

    public int Count()
        => _dbContext.Contact.Count();

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
