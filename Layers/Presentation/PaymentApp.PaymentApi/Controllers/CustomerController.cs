using MediatR;
using PaymentApp.PaymentApi.Controllers.Base;

namespace PaymentApp.PaymentApi.Controllers
{
    public class CustomerController : BaseApiController
    {
        public CustomerController(IMediator mediator) : base(mediator) { }

    }
}
