using System.Linq.Expressions;
namespace Common.Domain.Repository;
public interface IBaseRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task<T> AddAndGetEntityAsync(T entity, CancellationToken cancellationToken);
    Task AddRangeAsync(List<T> entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
    IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null, CancellationToken cancellationToken=default);
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task UpdateRangeAsync(List<T> entities, CancellationToken cancellationToken);
    Task DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
    IQueryable<T> Entity { get; }
    void DetachAllEntities();
    Task SaveChangeAsync(CancellationToken cancellationToken);
    Task<T> FirstAsync(CancellationToken cancellationToken);
    T FindAsNoTrackingAsync(Guid key, CancellationToken cancellationToken);
    Task<int> TotalCount(CancellationToken cancellationToken);
}
