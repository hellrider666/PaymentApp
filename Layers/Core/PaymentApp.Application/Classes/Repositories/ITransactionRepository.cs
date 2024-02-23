using PaymentApp.Application.Classes.Repositories.Base;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Classes.Repositories
{
    public interface ITransactionRepository : IBaseRepostiory<TransactionEntity>
    {
        Task<TransactionEntity> GetByTransactionNumber(string transactionNumber, CancellationToken cancellationToken);
        Task<List<TransactionEntity>> GetByFilterCriteriasAsync(CancellationToken cancellationToken, string senderNumber = null, string RecipientNumber = null,
            string TransactionNumber = null, DateTime? StartDateTime = null, DateTime? EndDateTime = null);
    }
}
