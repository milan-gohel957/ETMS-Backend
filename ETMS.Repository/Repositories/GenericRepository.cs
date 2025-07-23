using System.Linq.Expressions;
using ETMS.Domain.Entities;
using ETMS.Repository.Context;
using ETMS.Repository.Helpers;
using ETMS.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETMS.Repository.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public IQueryable<T> GetNonDeletedQuery()
    {
        return _dbSet.Where(x => !x.IsDeleted);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate == null) return false;
        return await GetNonDeletedQuery().AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate == null) return 0;
        return await GetNonDeletedQuery().CountAsync(predicate);
    }

    public void SoftDelete(T entity)
    {

        entity.IsDeleted = true;
        _dbSet.Update(entity);
    }


    // This is the most efficient way to soft delete by ID
    public Task SoftDeleteByIdAsync(int id)
    {
        // 1. Create a "stub" entity with only the Primary Key.
        //    We need the 'new()' constraint on T for this to work.
        T entityToUpdate = new() { Id = id };

        // 2. Attach the stub to the DbContext. EF Core starts tracking it,
        //    but thinks its state is 'Unchanged'.
        _dbSet.Attach(entityToUpdate);

        // 3. Now, change the property. EF Core's change tracker will see
        //    this one property change and mark the entity as 'Modified'.
        entityToUpdate.IsDeleted = true;

        // When SaveChangesAsync() is called, EF Core will generate a targeted
        // UPDATE statement like: "UPDATE [TableName] SET IsDeleted = 1 WHERE Id = @p0"
        // without ever needing to SELECT the row first.

        // No awaitable I/O is performed here, so we can return a completed task.
        return Task.CompletedTask;
    }
    public Task SoftDeleteRangeByIds(List<int> ids)
    {
        for (int i = 0; i < ids.Count; i++)
        {
            T entityToUpdate = new() { Id = ids[i] };
            _dbSet.Attach(entityToUpdate);
            entityToUpdate.IsDeleted = true;
        }
        return Task.CompletedTask;
    }
    public void SoftDeleteRange(IEnumerable<T> entities)
    {
        for (int i = 0; i < entities.Count(); i++)
        {
            entities.ElementAt(i).IsDeleted = true;
        }

        _dbSet.UpdateRange(entities);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return GetNonDeletedQuery().AnyAsync(e => e.Id == id && !e.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await GetNonDeletedQuery().Where(e => !e.IsDeleted).Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await GetNonDeletedQuery().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate == null) return await GetNonDeletedQuery().ToListAsync(cancellationToken);
        return await GetNonDeletedQuery().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllWithIncludesAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includes)
    {
        var query = predicate == null ? GetNonDeletedQuery() : GetNonDeletedQuery().Where(predicate);
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await GetNonDeletedQuery()
            .Where(e => e.Id == id && !e.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<T?> GetByIdWithIncludesAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        var query = GetNonDeletedQuery().Where(e => e.Id == id && !e.IsDeleted);

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<PaginationDto<T>> GetPagedAsync(
     int pageNumber,
     int pageSize,
     Expression<Func<T, bool>>? filter = null,
     Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
     params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = GetNonDeletedQuery();

        // Apply Includes
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        // Apply filter
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Count before paging
        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        // Clamp pageNumber to valid range
        if (pageNumber < 1)
            pageNumber = 1;
        if (pageNumber > totalPages && totalPages > 0)
            pageNumber = totalPages;

        // Apply ordering
        if (orderBy != null)
        {
            query = orderBy(query);
        }

        // Paging
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Return paginated result
        return new PaginationDto<T>
        {
            Items = items,
            Page = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
    public IQueryable<T> Query()
    {
        return GetNonDeletedQuery().AsQueryable();
    }

    public IQueryable<T> QueryWithIncludes(params Expression<Func<T, object>>[] includes)
    {
        var query = GetNonDeletedQuery().AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query;
    }

    public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await GetNonDeletedQuery().SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public IQueryable<T> Table => _dbSet.AsQueryable();
}