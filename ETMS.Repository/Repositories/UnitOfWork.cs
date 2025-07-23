using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ETMS.Repository.Repositories.Interfaces;
using ETMS.Domain.Entities;
using ETMS.Repository.Context;

namespace ETMS.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;
        private readonly Dictionary<Type, object> _repositories;
        private bool _disposed;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repositories = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Generic method to get repository instance
        /// </summary>
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity);
            
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositoryType.MakeGenericType(typeof(TEntity)), _context);
                
                _repositories[type] = repositoryInstance!;
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }

        /// <summary>
        /// Save all changes to the database
        /// </summary>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // Check if cancellation is requested before saving
                cancellationToken.ThrowIfCancellationRequested();
                
                // Set audit fields (CreatedAt, UpdatedAt, etc.) before saving
                SetAuditFields();
                
                // Save changes with cancellation token
                var result = await _context.SaveChangesAsync(cancellationToken);
                
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency conflicts
                throw new InvalidOperationException("A concurrency conflict occurred.", ex);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                throw new InvalidOperationException("An error occurred while saving changes.", ex);
            }
            catch (OperationCanceledException)
            {
                // Log cancellation if needed
                throw; // Re-throw to let the caller handle it
            }
        }

        /// <summary>
        /// Begin a new database transaction
        /// </summary>
        public async  Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            // Check if cancellation is requested
            cancellationToken.ThrowIfCancellationRequested();
            
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <summary>
        /// Commit the current transaction
        /// </summary>
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // Check if cancellation is requested
                cancellationToken.ThrowIfCancellationRequested();
                
                if (_transaction == null)
                {
                    throw new InvalidOperationException("No transaction is in progress.");
                }

                await _transaction.CommitAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                // If cancelled, attempt to rollback
                await RollbackAsync(CancellationToken.None); // Use None to ensure rollback completes
                throw;
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        /// <summary>
        /// Rollback the current transaction
        /// </summary>
        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // Note: We might not want to respect cancellation during rollback
                // to ensure data consistency
                if (_transaction == null)
                {
                    return; // Nothing to rollback
                }

                await _transaction.RollbackAsync(cancellationToken);
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        /// <summary>
        /// Execute work within a transaction with automatic rollback on failure
        /// </summary>
        public async Task<T> ExecuteInTransactionAsync<T>(
            Func<CancellationToken, Task<T>> operation, 
            CancellationToken cancellationToken = default)
        {
            await BeginTransactionAsync(cancellationToken);
            
            try
            {
                var result = await operation(cancellationToken);
                await CommitAsync(cancellationToken);
                return result;
            }
            catch
            {
                await RollbackAsync(CancellationToken.None); // Ensure rollback completes
                throw;
            }
        }

        /// <summary>
        /// Execute work within a transaction with automatic rollback on failure (void return)
        /// </summary>
        public async Task ExecuteInTransactionAsync(
            Func<CancellationToken, Task> operation, 
            CancellationToken cancellationToken = default)
        {
            await BeginTransactionAsync(cancellationToken);
            
            try
            {
                await operation(cancellationToken);
                await CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackAsync(CancellationToken.None); // Ensure rollback completes
                throw;
            }
        }

        /// <summary>
        /// Set audit fields for entities before saving
        /// </summary>
        private void SetAuditFields()
        {
            var entries = _context.ChangeTracker.Entries<BaseEntity>();
            var currentTime = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = currentTime;
                        entry.Entity.UpdatedAt = currentTime;
                        break;
                    
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = currentTime;
                        // Prevent modification of CreatedAt
                        entry.Property(x => x.CreatedAt).IsModified = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Dispose the current transaction
        /// </summary>
        private async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _context?.Dispose();
                }
                _disposed = true;
            }
        }

        #endregion
    }
}