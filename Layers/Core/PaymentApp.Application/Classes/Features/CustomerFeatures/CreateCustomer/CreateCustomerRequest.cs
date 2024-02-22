using MediatR;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.CreateCustomer
{
    public class CreateCustomerRequest : IRequest<CreateCustomerResponse>
    {
        public string Name { get; set; }
        public string AccountNumber { get; set; }
    }
}
