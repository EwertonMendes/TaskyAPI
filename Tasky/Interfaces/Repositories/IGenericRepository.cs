using System.Linq.Expressions;
using Tasky.Models;

namespace Tasky.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetById(int id);
        Task<T?> GetById(int id, params Expression<Func<T, object>>[] includes);
        Task<IAsyncEnumerable<T>> GetAll();
        Task<IAsyncEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task<IAsyncEnumerable<T>> Find(Expression<Func<T, object>>[]? includes, Expression<Func<T, bool>> expression);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Remove(T entity);
    }
}
