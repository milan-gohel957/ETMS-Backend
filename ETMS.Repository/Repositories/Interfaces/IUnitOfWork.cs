using ETMS.Domain.Entities;

namespace ETMS.Repository.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    Task<T> ExecuteInTransactionAsync<T>(
          Func<CancellationToken, Task<T>> operation,
          CancellationToken cancellationToken = default);

}
