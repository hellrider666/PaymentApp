using PaymentApp.Application.Classes.Repositories.Base;
using PaymentApp.Domain.Entities.Base;

namespace PaymentApp.Application.Classes.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<TResult> DoWork<TRepository, TEntity, TResult>(Func<TRepository, Task<TResult>> action)
            where TRepository : class, IBaseRepostiory<TEntity>
            where TEntity : BaseEntity;

        void DoWork<TRepository, TEntity>(Action<TRepository> action)
            where TRepository : class, IBaseRepostiory<TEntity>
            where TEntity : BaseEntity;

        void DoWork(Action action);

        Task<T> BeginTransactionAsync<T>(Func<Task<T>> action);

        void BeginTransactionSync(Action action);

        TResult BeginTransactionSync<TResult>(Func<TResult> action);
    }
}
