using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tasky.Data;
using Tasky.Interfaces.Models;
using Tasky.Interfaces.Repositories;
using Tasky.Utilities;

namespace Tasky.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    internal TaskyContext _context;
    internal DbSet<T> _DbSet;

    public GenericRepository(TaskyContext context)
    {
        _context = context;
        _DbSet = context.Set<T>();
    }

    public async Task<T> Add(T entity, params Expression<Func<T, object>>[] includes)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();

        var entry = _context.Entry(entity);

        foreach (var include in includes)
        {
            
            entry.Reference(include).Load();
        }

        return entity;
    }

    public async Task<T> Update(T entity, params Expression<Func<T, object>>[] includes)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();

        var entry = _context.Entry(entity);

        foreach (var include in includes)
        {
            entry.Reference(include).Load();
        }

        return entity;
    }

    public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> filter = null)
    {
        var query = _DbSet.AsQueryable();

        if(filter != null)
        {
            query = query.Where(filter).AsNoTracking();
        }
        return query.AsEnumerable();
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate) 
        => await _DbSet.SingleOrDefaultAsync(predicate);

    public async Task<IAsyncEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, 
        params Expression<Func<T, object>>[] includes) 
        => _DbSet.Includes(includes).Where(filter).AsAsyncEnumerable();
    

    public async Task<T?> GetById(int id)
    {
        return await _DbSet.FindAsync(id);
    }

    public async Task<T?> GetById(int id, params Expression<Func<T, object>>[] includes)
    {
        return await _DbSet.Includes(includes)
            .Select(x => x as IModel).FirstOrDefaultAsync(x => x.Id == id) as T;
    }

    public async Task Remove(T entity)
    {
        _DbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public Task<T?> FindBy(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
    {
        return _DbSet.Includes(includes).FirstOrDefaultAsync(expression);
    }
}
