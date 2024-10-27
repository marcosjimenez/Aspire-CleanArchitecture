using CleanArchitectureSample.Core.Aggregates;
using System.Linq.Expressions;

namespace CleanArchitectureSample.Application.Contracts
{
    public interface IBaseRepository<TKey,TEntity>
        where TEntity: BaseEntity
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity? GetById(int id);
        TEntity? GetByIdWithIncludes(int id);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetByIdWithIncludesAsync(int id);
        bool Exists(int id);
        TKey Create(TEntity entity);
        void Update(TEntity entity);
        bool Delete(TKey id);
        int Save();
        Task<int> SaveAsync();
        int Count();
        public TEntity? Select(Expression<Func<TEntity?, bool>> predicate);
        public Task<TEntity?> SelectAsync(Expression<Func<TEntity?, bool>> predicate);
    }
}
