using System.Linq.Expressions;
using ETMS.Domain.Entities;
using ETMS.Repository.Helpers;

namespace ETMS.Repository.Repositories.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
  Task SoftDeleteRangeByIds(List<int> ids);
  Task SoftDeleteByIdAsync(int id);
  Task SoftDeleteRangeAsync(Expression<Func<T, bool>>? predicate = null);


  // Read operations
  Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
  Task<T?> GetByIdWithIncludesAsync(int id, params Expression<Func<T, object>>[] includes);
  Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);

  Task<IEnumerable<T>> GetAllWithIncludesAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes);

  Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);

  // Query operations
  Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
  Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default);


  Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
  IQueryable<T> Query();
  IQueryable<T> QueryWithIncludes(params Expression<Func<T, object>>[] includes);

  // Pagination
  Task<PaginationDto<T>> GetPagedAsync(
      int pageNumber,
      int pageSize,
      Expression<Func<T, bool>>? filter = null,
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
      params Expression<Func<T, object>>[] includes);

  // Write operations
  Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
  Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
  void Update(T entity);
  void UpdateRange(IEnumerable<T> entities);
  void SoftDelete(T entity);
  void SoftDeleteRange(IEnumerable<T> entities);

  // Aggregate operations
  Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);
  Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);

  IQueryable<T> Table { get; }
}
