using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Persistence.Classes.Context;

namespace PaymentApp.Persistence.Classes.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IAppDbContext _DbContext;
        public UnitOfWork(IAppDbContext context, ICustomerRepository customerRepository, ITransactionRepository transactionRepository)
        {
            _DbContext = context;

            CustomerRepository = customerRepository;
            TransactionRepository = transactionRepository;
        }

        public ICustomerRepository CustomerRepository { get;  }
        public ITransactionRepository TransactionRepository { get; }

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

        private async Task<int> SaveChangesAsync()
        {
            return await _DbContext.SaveChangesAsync();
        }

        public async Task<T> BeginTransaction<T>(Func<Task<T>> func)
        {
            using (var transaction = _DbContext.BeginTransaction())
            {
                try
                {
                    var result = await func();

                    await SaveChangesAsync();
                    transaction.Commit();

                    return result;
                }
                catch (Exception exc)
                {
                    transaction.Rollback();
                    throw new Exception();
                }
            }
        }

        public async Task<T> BeginTransaction<T>(Func<T> func)
        {
            using (var transaction = _DbContext.BeginTransaction())
            {
                try
                {
                    var result = func();

                    await SaveChangesAsync();
                    transaction.Commit();

                    return result;
                }
                catch (Exception exc)
                {
                    transaction.Rollback();
                    throw new Exception();
                }
            }
        }
    }
}
