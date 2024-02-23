namespace PaymentApp.Application.Classes.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public ICustomerRepository CustomerRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
        public Task<T> BeginTransaction<T>(Func<Task<T>> func);
        public Task<T> BeginTransaction<T>(Func<T> func);
    }
}
