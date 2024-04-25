using MediatR;
using PaymentApp.Application.Classes.Abstract.Interfaces;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Queries.GetCustomerByNumber
{
    public class GetCustomerByNumberRequest : IAppRequest<GetCustomerByNumberResponse>
    {
        public string AccountNumber { get; set; }
    }
}
