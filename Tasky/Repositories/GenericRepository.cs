using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tasky.Data;
using Tasky.Interfaces.Models;
using Tasky.Interfaces.Repositories;
using Tasky.Utilities;

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

    public T? Add(T entity)
    {
        var newRecord = _DbSet.Add(entity).Entity;
        _context.SaveChanges();

        return newRecord;
    }

    public T? Update(T entity)
    {
        var updatedRecord = _DbSet.Update(entity).Entity;
        _context.SaveChanges();
        return updatedRecord;
    }

    public IEnumerable<T?> Find(Expression<Func<T, object>>[]? includes = null, Expression<Func<T, bool>> filter = null)
    {
        var query = _DbSet.AsQueryable();

        if(filter != null)
        {
            query = query.Where(filter).AsNoTracking();
        }
        return query.AsEnumerable().AsEnumerable();
    }

    public IEnumerable<T?> GetAll()
    {
        return _DbSet.AsEnumerable();
    }

    public IEnumerable<T?> GetAll(params Expression<Func<T, object>>[] includes)
    {
        return _DbSet.Includes(includes).AsEnumerable();
    }

    public T? GetById(int id)
    {
        return _DbSet.Find(id);
    }

    public T? GetById(int id, params Expression<Func<T, object>>[] includes)
    {
        return _DbSet.Includes(includes)
            .Select(x => x as IModel).FirstOrDefault(x => x.Id == id) as T;
    }

    public void Remove(T entity)
    {
        _DbSet.Remove(entity);
        _context.SaveChanges();
    }
}
