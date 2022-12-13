using System.Linq.Expressions;

namespace Tasky.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T? GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        T Add(T entity);
        T Update(T entity);
        void Remove(T entity);
    }
}
