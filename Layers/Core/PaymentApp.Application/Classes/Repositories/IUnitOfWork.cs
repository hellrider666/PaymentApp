namespace PaymentApp.Application.Classes.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public ICustomerRepository CustomerRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
        public Task<int> SaveChangesAsync();
    }
}
