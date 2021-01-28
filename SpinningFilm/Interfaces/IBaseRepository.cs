using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinningFilm.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> GetByIdAsync(string id);
        Task<IReadOnlyList<TEntity>> ListAllAsync();
        Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec);
        Task<TEntity> SingleOrDefaultAsync(ISpecification<TEntity> spec);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IQueryable<TEntity>> ListQueryableAsync(ISpecification<TEntity> spec);
    }
}
