using System.Linq.Expressions;
using System.Threading;
using Common.Domain;
using Common.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Repository;

public class BaseRepository<T, TContext> : IBaseRepository<T> where T : BaseEntity where TContext : DbContext
{
    private readonly TContext _context;

    public BaseRepository(TContext context)
    {
        _context = context;
    }

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().AnyAsync(expression, cancellationToken);
    }

    public virtual IQueryable<T> Entity => _context.Set<T>();


    public virtual async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);

    }

    public async Task<T> AddAndGetEntityAsync(T entity, CancellationToken cancellationToken)
    {
        T AddedEntity = entity;

        await _context.Set<T>().AddAsync(entity, cancellationToken);
        return AddedEntity;
    }
    public void DetachAllEntities()
    {
        var ChangedEntriesCopy = _context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added ||
                        e.State == EntityState.Modified ||
                        e.State == EntityState.Deleted)
            .ToList();

        foreach (var Entry in ChangedEntriesCopy)
            Entry.State = EntityState.Detached;
    }
    public virtual async Task AddRangeAsync(List<T> entites, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddRangeAsync(entites, cancellationToken);
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        await Task.FromCanceled(cancellationToken);
        _context.Set<T>().Remove(entity);

    }

    public virtual async Task DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken)
    {
        await Task.FromCanceled(cancellationToken);
        _context.Set<T>().RemoveRange(entities);

    }

    public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null, CancellationToken cancellationToken=default)
    {
        Task.FromCanceled<T>(cancellationToken);
        var result = _context.Set<T>().AsNoTracking();
        if (expression != null)
            result = result.Where(expression);
        return result;
    }

    public virtual async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {

        return await _context.Set<T>().FindAsync(id, cancellationToken);
    }

    public virtual async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {

        return await _context.Set<T>().FirstOrDefaultAsync(expression, cancellationToken);
    }

    public virtual async Task SaveChangeAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<T> FirstAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FirstAsync(cancellationToken);
    }

    public virtual async Task<int> TotalCount(CancellationToken cancellationToken)
    {
        return await Entity.CountAsync(cancellationToken);
    }

    public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        Task.FromCanceled(cancellationToken);
        _context.ChangeTracker.Clear();
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Detached;
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }
    public async ValueTask DisposeAsync()
    {
        if (_context != null)
        {
            await _context.DisposeAsync();
        }
    }



    public virtual T FindAsNoTrackingAsync(Guid key, CancellationToken cancellationToken)
    {
        _context.ChangeTracker.DetectChanges();
        Task.FromCanceled<T>(cancellationToken);    
        var entity = _context.Set<T>().Find(key);
        _context.Entry(entity).State = EntityState.Deleted;
        return entity;
    }

    public async Task UpdateRangeAsync(List<T> entities,CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            await UpdateAsync(entity, cancellationToken);
        }
    }
}