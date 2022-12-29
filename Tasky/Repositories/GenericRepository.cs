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

    public async Task<T> Add(T entity)
    {
        var newRecord = _DbSet.Add(entity).Entity;
        await _context.SaveChangesAsync();

        return newRecord;
    }

    public async Task<T> Update(T entity)
    {
        var updatedRecord = _DbSet.Update(entity).Entity;
        await _context.SaveChangesAsync();
        return updatedRecord;
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

    public async Task<IAsyncEnumerable<T>> GetAll() => _DbSet.AsAsyncEnumerable();

    public async Task<IAsyncEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
    {
        return _DbSet.Includes(includes).AsAsyncEnumerable();
    }

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

    public Task<T?> FindBy(Expression<Func<T, bool>> expression)
    {
        return _DbSet.FirstOrDefaultAsync(expression);
    }
}
