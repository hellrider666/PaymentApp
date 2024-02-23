using MediatR;

namespace PaymentApp.Application.Classes.Features.TransactionFeatures.Queries.GetTransactions
{
    public class GetTransactionsRequest : IRequest<GetTransactionsResponse>
    {
        public string? SenderNumber { get; set; }
        public string? RecipientNumber { get; set; }
        public string? TransactionNumber { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}
