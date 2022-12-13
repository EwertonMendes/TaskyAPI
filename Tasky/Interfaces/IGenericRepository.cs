using System.Linq.Expressions;
using Tasky.Models;

namespace Tasky.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T? GetById(int id);
        T? GetById(int id, params Expression<Func<T, object>>[] includes);
        IEnumerable<T?> GetAll();
        IEnumerable<T?> GetAll(params Expression<Func<T, object>>[] includes);
        IEnumerable<T?> Find(Expression<Func<T, object>>[]? includes, Expression<Func<T, bool>> expression);
        T? Add(T entity);
        T? Update(T entity);
        void Remove(T entity);
    }
}
