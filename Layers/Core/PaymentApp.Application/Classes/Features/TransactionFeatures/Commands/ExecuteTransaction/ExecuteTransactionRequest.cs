using MediatR;

namespace PaymentApp.Application.Classes.Features.TransactionFeatures.Commands.ExecuteTransaction
{
    public class ExecuteTransactionRequest : IRequest<ExecuteTransactionResponse>
    {
        public string SenderNumber { get; set; }
        public string RecipientNumber { get; set; }
        public string TransactionNumber { get; set; }
        public decimal Sum { get; set; }
    }
}
