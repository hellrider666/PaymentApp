using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Domain.Entities;
using PaymentApp.Persistence.Classes.Context;
using PaymentApp.Persistence.Classes.Repositories.Base;

namespace PaymentApp.Persistence.Classes.Repositories
{
    public class TransactionRepository : BaseRepository<TransactionEntity>, ITransactionRepository
    {
        public TransactionRepository(IAppDbContext dbContext) : base(dbContext) { }
    }
}
