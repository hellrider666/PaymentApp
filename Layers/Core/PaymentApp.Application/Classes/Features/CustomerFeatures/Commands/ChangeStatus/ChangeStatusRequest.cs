using MediatR;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.ChangeStatus
{
    public class ChangeStatusRequest : IRequest<ChangeStatusResponse>
    {
        public string AccountNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
