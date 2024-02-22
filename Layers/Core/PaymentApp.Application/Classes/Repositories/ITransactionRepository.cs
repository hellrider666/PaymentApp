using PaymentApp.Application.Classes.Repositories.Base;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Classes.Repositories
{
    public interface ITransactionRepository : IBaseRepostiory<TransactionEntity>
    {
    }
}
