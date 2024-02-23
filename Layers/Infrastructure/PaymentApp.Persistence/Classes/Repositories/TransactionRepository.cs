using Microsoft.EntityFrameworkCore;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Domain.Entities;
using PaymentApp.Persistence.Classes.Context;
using PaymentApp.Persistence.Classes.Repositories.Base;

namespace PaymentApp.Persistence.Classes.Repositories
{
    public class TransactionRepository : BaseRepository<TransactionEntity>, ITransactionRepository
    {
        public TransactionRepository(IAppDbContext dbContext) : base(dbContext) { }

        public async Task<TransactionEntity> GetByTransactionNumber(string transactionNumber, CancellationToken cancellationToken)
        {
            return await GetInternal().FirstOrDefaultAsync(x => x.TransactionNumber == transactionNumber, cancellationToken);
        }

        public async Task<List<TransactionEntity>> GetByFilterCriteriasAsync(CancellationToken cancellationToken, string senderNumber = null, string RecipientNumber = null,
            string TransactionNumber = null, DateTime? StartDateTime = null, DateTime? EndDateTime = null)
        {
            var transactions = GetInternal().Include(x => x.SenderCustomer).Include(x => x.RecipientCustomer) as IQueryable<TransactionEntity>;              

            if (!transactions.Any()) return await transactions.ToListAsync();

            if (!string.IsNullOrWhiteSpace(senderNumber))
                transactions = transactions.Where(x => x.SenderCustomer.AccountNumber == senderNumber);

            if (!transactions.Any()) return await transactions.ToListAsync();

            if (!string.IsNullOrWhiteSpace(RecipientNumber))
                transactions = transactions.Where(x => x.RecipientCustomer.AccountNumber == RecipientNumber);

            if (!transactions.Any()) return await transactions.ToListAsync();

            if (!string.IsNullOrWhiteSpace(TransactionNumber))
                transactions = transactions.Where(x => x.TransactionNumber == TransactionNumber);

            if (!transactions.Any()) return await transactions.ToListAsync();

            if (StartDateTime != null && StartDateTime != DateTime.MinValue)
                transactions = transactions.Where(x => x.ExecutingDateTime >= StartDateTime.Value);

            if (!transactions.Any()) return await transactions.ToListAsync();

            if (EndDateTime != null && EndDateTime != DateTime.MinValue)
                transactions = transactions.Where(x => x.ExecutingDateTime <= EndDateTime.Value);

            return await transactions.ToListAsync();
        }
    }
}
