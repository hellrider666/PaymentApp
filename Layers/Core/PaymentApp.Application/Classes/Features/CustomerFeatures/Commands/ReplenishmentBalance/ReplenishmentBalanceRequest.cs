using MediatR;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.ReplenishmentBalance
{
    public class ReplenishmentBalanceRequest : IRequest<ReplenishmentBalanceResponse>
    {
        public string AccountNumber { get; set; }
        public decimal Sum { get; set; }
    }
}
