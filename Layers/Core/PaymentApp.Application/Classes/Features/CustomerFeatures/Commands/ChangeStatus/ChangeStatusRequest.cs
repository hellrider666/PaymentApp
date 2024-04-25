using MediatR;
using PaymentApp.Application.Classes.Abstract.Interfaces;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.ChangeStatus
{
    public class ChangeStatusRequest : IAppRequest<ChangeStatusResponse>
    {
        public string AccountNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
