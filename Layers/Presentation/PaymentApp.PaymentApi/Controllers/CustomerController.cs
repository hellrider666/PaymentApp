using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentApp.Application.Classes.Features.CustomerFeatures.CreateCustomer;
using PaymentApp.PaymentApi.Controllers.Base;
using System.Net;

namespace PaymentApp.PaymentApi.Controllers
{
    public class CustomerController : BaseApiController
    {
        public CustomerController(IMediator mediator) : base(mediator) { }

        [HttpPost(Name = "CreateCustomer"), ProducesResponseType(typeof(CreateCustomerResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateCustomer([FromHeader]string API_KEY, [FromBody]CreateCustomerRequest request)
        {
            return await Execute<CreateCustomerRequest, CreateCustomerResponse>(request);
        }
    }
}
