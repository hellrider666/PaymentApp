using MediatR;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Queries.GetCustomerByNumber
{
    public class GetCustomerByNumberRequest : IRequest<GetCustomerByNumberResponse>
    {
        public string AccountNumber { get; set; }
    }
}
