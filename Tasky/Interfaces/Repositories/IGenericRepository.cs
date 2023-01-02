using System.Linq.Expressions;
using Tasky.Models;

namespace Tasky.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetById(int id);
    Task<T?> GetById(int id, params Expression<Func<T, object>>[] includes);
    Task<IAsyncEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> Where(Expression<Func<T, bool>> expression);
    Task<T?> FindBy(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
    Task<T> Add(T entity, params Expression<Func<T, object>>[] includes);
    Task<T> Update(T entity, params Expression<Func<T, object>>[] includes);
    Task Remove(T entity);
}
