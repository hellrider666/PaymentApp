using MediatR;
using PaymentApp.Application.Classes.Abstract.Interfaces;

namespace PaymentApp.Application.Classes.Features.TransactionFeatures.Commands.ExecuteTransaction
{
    public class ExecuteTransactionRequest : IAppRequest<ExecuteTransactionResponse>
    {
        public string SenderNumber { get; set; }
        public string RecipientNumber { get; set; }
        public string TransactionNumber { get; set; }
        public decimal Sum { get; set; }
    }
}
