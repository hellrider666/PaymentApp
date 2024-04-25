using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.ChangeStatus;
using PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.CreateCustomer;
using PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.ReplenishmentBalance;
using PaymentApp.Application.Classes.Features.CustomerFeatures.Queries.GetCustomerByNumber;
using PaymentApp.Application.Classes.Managers.Request;
using PaymentApp.PaymentApi.Controllers.Base;
using System.Net;

namespace PaymentApp.PaymentApi.Controllers
{
    public class CustomerController : BaseApiController
    {
        public CustomerController(IRequestManager requestManager) : base(requestManager) { }

        [HttpPost(Name = "CreateCustomer"), ProducesResponseType(typeof(CreateCustomerResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateCustomer([FromHeader]string API_KEY, [FromBody]CreateCustomerRequest request)
        {
            return await Execute<CreateCustomerRequest, CreateCustomerResponse>(request);
        }

        [HttpPut(Name = "ChangeStatus"), ProducesResponseType(typeof(ChangeStatusResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangeStatus([FromHeader] string API_KEY, [FromBody] ChangeStatusRequest request)
        {
            return await Execute<ChangeStatusRequest, ChangeStatusResponse>(request);
        }

        [HttpGet(Name = "GetCustomerByNumber"), ProducesResponseType(typeof(GetCustomerByNumberResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerByNumber([FromHeader] string API_KEY, [FromQuery] GetCustomerByNumberRequest request)
        {
            return await Execute<GetCustomerByNumberRequest, GetCustomerByNumberResponse>(request);
        }

        [HttpPut(Name = "ReplenishmentBalance"), ProducesResponseType(typeof(ReplenishmentBalanceResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ReplenishmentBalance([FromHeader] string API_KEY, [FromBody] ReplenishmentBalanceRequest request)
        {
            return await Execute<ReplenishmentBalanceRequest, ReplenishmentBalanceResponse>(request);
        }
    }
}
