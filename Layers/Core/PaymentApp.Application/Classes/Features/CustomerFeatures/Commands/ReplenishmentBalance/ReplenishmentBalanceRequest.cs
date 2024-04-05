using MediatR;
using PaymentApp.Application.Classes.Interfaces;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.ReplenishmentBalance
{
    public class ReplenishmentBalanceRequest : IAppRequest<ReplenishmentBalanceResponse>
    {
        public string AccountNumber { get; set; }
        public decimal Sum { get; set; }
    }
}
