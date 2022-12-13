using Microsoft.EntityFrameworkCore;
using Tasky.Data;
using Tasky.Interfaces;

namespace Tasky.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly TaskyContext _context;
    protected readonly DbSet<T> _DbSet;

    public GenericRepository(TaskyContext context)
    {
        _context = context;
        _DbSet = context.Set<T>();
    }

    public T Add(T entity)
    {
        var newRecord = _DbSet.Add(entity).Entity;
        _context.SaveChanges();

        return newRecord;
    }

    public T Update(T entity)
    {
        var updatedRecord = _DbSet.Update(entity).Entity;
        _context.SaveChanges();
        return updatedRecord;
    }

    public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> filter = null)
    {
        var query = _DbSet.AsQueryable();

        if(filter != null)
        {
            query = query.Where(filter).AsNoTracking();
        }
        return query.ToList();
    }

    public virtual IEnumerable<T> GetAll()
    {
        return _DbSet.ToList();
    }

    public virtual T? GetById(int id)
    {
        return _DbSet.Find(id);
    }

    public void Remove(T entity)
    {
        _DbSet.Remove(entity);
        _context.SaveChanges();
    }
}
