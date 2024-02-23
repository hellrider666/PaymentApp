using PaymentApp.Application.Classes.DTOs;

namespace PaymentApp.Application.Classes.Features.TransactionFeatures.Queries.GetTransactions
{
    public class GetTransactionsResponse
    {
        public List<TransactionDTO> Transactions { get; set; }
    }
}
