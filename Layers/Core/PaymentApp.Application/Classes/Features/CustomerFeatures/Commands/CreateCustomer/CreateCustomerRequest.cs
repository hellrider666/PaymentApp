using MediatR;
using PaymentApp.Application.Classes.Abstract.Interfaces;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.CreateCustomer
{
    public class CreateCustomerRequest : IAppRequest<CreateCustomerResponse>
    {
        public string Name { get; set; }
        public string AccountNumber { get; set; }
    }
}
