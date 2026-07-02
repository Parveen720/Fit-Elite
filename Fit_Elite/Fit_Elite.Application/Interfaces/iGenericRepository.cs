using System.Linq.Expressions;

namespace Fit_Elite.Application.Interfaces
{
    public interface iGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(long id);

        Task<T?> FindSingleAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes
        );

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task SaveAsync(); 
    }
}