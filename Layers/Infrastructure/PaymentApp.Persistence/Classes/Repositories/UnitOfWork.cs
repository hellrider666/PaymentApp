using Microsoft.Extensions.DependencyInjection;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Application.Classes.Repositories.Base;
using PaymentApp.Domain.Entities.Base;
using PaymentApp.Persistence.Classes.Context;

namespace PaymentApp.Persistence.Classes.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IAppDbContext _DbContext;
        private readonly IServiceProvider _serviceProvider;
        public UnitOfWork(IAppDbContext context, IServiceProvider serviceProvider)
        {
            _DbContext = context;

            _serviceProvider = serviceProvider;
        }

        #region SAVE METHODS

        private async Task<int> SaveChangesAsync()
        {
            return await _DbContext.SaveChangesAsync();
        }

        private int SaveChangesSync()
        {
            return _DbContext.SaveChangesSync();
        }

        #endregion

        #region DOWORK METHODS

        public async Task<TResult> DoWork<TRepository, TEntity, TResult>(Func<TRepository, Task<TResult>> action) 
            where TRepository : class, IBaseRepostiory<TEntity>
            where TEntity : BaseEntity
        {
            try
            {
                var repositoryInstance = _serviceProvider.GetService<TRepository>();

                if (repositoryInstance == null)
                    throw new Exception($"Служба {nameof(repositoryInstance)} не зарегистрирована");

                return await BeginTransactionAsync(async () =>
                {
                    return await action(repositoryInstance);
                });
            }
            catch
            {
                throw;
            }
        }
        public async Task<TResult> DoWork<TRepository, TEntity, TResult>(Func<TRepository, TResult> action)
            where TRepository : class, IBaseRepostiory<TEntity>
            where TEntity : BaseEntity
        {
            try
            {
                var repositoryInstance = _serviceProvider.GetService<TRepository>();

                if (repositoryInstance == null)
                    throw new Exception($"Служба {nameof(repositoryInstance)} не зарегистрирована");

                return await BeginTransactionAsync(() =>
                {
                    return action(repositoryInstance);

                });
            }
            catch
            {
                throw;
            }
        }

        public async Task DoWork<TRepository, TEntity>(Func<TRepository> action)
            where TRepository : class, IBaseRepostiory<TEntity>
            where TEntity : BaseEntity
        {
            try
            {
                var repositoryInstance = _serviceProvider.GetService<TRepository>();

                if (repositoryInstance == null)
                    throw new Exception($"Служба {nameof(repositoryInstance)} не зарегистрирована");

                await BeginTransactionAsync(() =>
                {
                    action();
                });
            }
            catch
            {
                throw;
            }
        }

        public async Task DoWork<TRepository, TEntity>(Func<TRepository, Task> action)
            where TRepository : class, IBaseRepostiory<TEntity>
            where TEntity : BaseEntity
        {
            try
            {
                var repositoryInstance = _serviceProvider.GetService<TRepository>();

                if (repositoryInstance == null)
                    throw new Exception($"Служба {nameof(repositoryInstance)} не зарегистрирована");

                await BeginTransactionAsync(async () =>
                {
                    await action(repositoryInstance);
                });

            }
            catch
            {
                throw;
            }
        }

        public void DoWork<TRepository, TEntity>(Action<TRepository> action) 
            where TRepository : class, IBaseRepostiory<TEntity>
            where TEntity : BaseEntity
        {
            try
            {
                var repositoryInstance = _serviceProvider.GetService<TRepository>();

                if (repositoryInstance == null)
                    throw new Exception($"Служба {nameof(repositoryInstance)} не зарегистрирована");

                BeginTransactionSync(() =>
                    {
                        action(repositoryInstance);
                    });
            }
            catch
            {
                throw;
            }
        }

        public void DoWork(Action action)
        {
            try
            {
                BeginTransactionSync(() =>
                {
                    action();
                });
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region TRANSACTIONS METHODS

        private async Task<T> BeginTransactionAsync<T>(Func<T> action)
        {
            using (var transaction = await _DbContext.BeginTransactionAsync())
            {
                try
                {
                    var result = action();

                    await SaveChangesAsync();
                    await transaction.CommitAsync();

                    return result;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private async Task<T> BeginTransactionAsync<T>(Func<Task<T>> action)
        {
            using (var transaction = await _DbContext.BeginTransactionAsync())
            {
                try
                {
                    var result = await action();

                    await SaveChangesAsync();
                    await transaction.CommitAsync();

                    return result;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private async Task BeginTransactionAsync(Action action)
        {
            using (var transaction = await _DbContext.BeginTransactionAsync())
            {
                try
                {
                    action();
                    await SaveChangesAsync();
                    await transaction.CommitAsync();

                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private void BeginTransactionSync(Action action)
        {
            using (var transaction = _DbContext.BeginTransaction())
            {
                try
                {
                    action();
                    SaveChangesSync();
                    transaction.Commit();
                    
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        #endregion

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _DbContext.DisposeContext();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
