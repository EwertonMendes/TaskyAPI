using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Tasky.Utilities;

public static class IncludeExtension
{
    public static IQueryable<TEntity> Includes<TEntity>(this DbSet<TEntity> dbSet,
                                            params Expression<Func<TEntity, object>>[] includes)
                                            where TEntity : class
    {
        IQueryable<TEntity> query = null;
        foreach (var include in includes)
        {
            query = dbSet.Include(include);
        }

        return query == null ? dbSet : query;
    }
}